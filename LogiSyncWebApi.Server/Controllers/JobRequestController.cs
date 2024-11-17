﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiSyncWebApi.Server.Models; 
using LogiSyncWebApi.Server.Shared;
using LogiSyncWebApi.Server.Models.DataPayloads;
using LogiSyncWebApi.Server.Migrations;
using System.Data.Common;
using System.Transactions;
using System.ComponentModel.Design;

namespace LogiSyncWebApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public DateTime SysDate = DateTime.Now.ToLocalTime();

        public JobRequestController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        #region GetAllJobRequests
        [HttpGet]
        [Route("GetAllJobRequests")]
        public IActionResult GetAllJobRequests()
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetAllJobRequests);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var jobRequests = db.JobRequests
                        .Include(jr => jr.PriceAgreement)
                        .Include(jr => jr.Truck)
                        .Include(jr => jr.Customer)
                        .Where(jr => jr.Status !="CANCELED")
                        .ToList();
                    executionResult.SetData(jobRequests);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region GetJobRequestById
        [HttpGet]
        [Route("GetJobRequestById/{jobRequestID}")]
        public IActionResult GetJobRequestById(string jobRequestID)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetJobRequestById);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var jobRequest = db.JobRequests
                        .Include(jr => jr.PriceAgreement)
                        .Include(jr => jr.Truck)
                        .Include(jr => jr.Customer)
                        .FirstOrDefault(jr => jr.JobRequestID == jobRequestID);
                    if (jobRequest == null)
                    {
                        return NotFound("Job Request not found");
                    }
                    executionResult.SetData(jobRequest);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        //#region GetCompanyJobRequest
        //[HttpGet]
        //[Route("GetCompanyJobRequest/{CompanyID}")]
        //public IActionResult GetCompanyJobRequest(string CompanyID)
        //{
        //    var executionResult = new ExecutionResult();
        //    string functionName = nameof(GetCompanyJobRequest);

        //    try
        //    {
        //        using (var db = new AppDbContext(_config))
        //        {

        //            var jobRequests = db.JobRequests
        //                .Include(jr => jr.PriceAgreement)
        //                .Include(jr => jr.Truck)
        //                .Include(jr => jr.Customer)
        //                .Where((jr => jr.Status != "CANCELED" && (jr.Status != "PENDING PAYMENTS") || (jr.Status == "PENDING PAYMENTS" && jr.AssignedCompany == CompanyID))
        //                )
        //                 .ToList();
        //            if (jobRequests == null || jobRequests.Count == 0)
        //            {
        //                return NotFound("No Job Requests Available For This Company");
        //            }

        //            foreach (var avilabeleJobs in jobRequests)
        //            {
        //                if(avilabeleJobs.PriceAgreement.CompanyID!= CompanyID)
        //                {
        //                    avilabeleJobs.PriceAgreement.CompanyPrice = 0;
        //                    avilabeleJobs.PriceAgreement.CustomerPrice = 0;
        //                    avilabeleJobs.PriceAgreement.AgreedPrice = 0;

        //                 }

        //                jobRequests= avilabeleJobs;  
        //            }

        //            executionResult.SetData(jobRequests); // Set the list of job requests
        //            return Ok(executionResult.GetServerResponse());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
        //        return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
        //    }
        //}
        //#endregion
        #region GetCompanyJobRequest
        [HttpGet]
        [Route("GetCompanyJobRequest/{CompanyID}")]
        public IActionResult GetCompanyJobRequest(string CompanyID)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetCompanyJobRequest);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    // Fetch all job requests that are relevant to the company
                    var jobRequests = db.JobRequests
                        .Include(jr => jr.PriceAgreement)
                        .Include(jr => jr.Truck)
                        .Include(jr => jr.Customer)
                        .Where(jr =>
                            jr.Status != "CANCELED" &&
                            (jr.Status != "PENDING PAYMENTS" ||
                            (jr.Status == "PENDING PAYMENTS" && jr.AssignedCompany == CompanyID)))
                        .ToList();

                    // Check if any job requests exist
                    if (jobRequests == null || jobRequests.Count == 0)
                    {
                        return NotFound("No Job Requests Available For This Company");
                    }


                    // Map PriceAgreement specific to the CompanyID and JobRequestID
                    foreach (var job in jobRequests)
                    {
                        var priceAgreement = db.PriceAgreements
                            .FirstOrDefault(pa => pa.CompanyID == CompanyID && pa.JobRequestID == job.JobRequestID);

                        // If no matching PriceAgreement exists, assign a default instance
                        if (priceAgreement == null)
                        {
                            priceAgreement = new RequestWithPayment
                            {
                                PriceAgreementID = Functions.GeneratePriceAgreementId(),
                                CompanyID = CompanyID,
                                JobRequestID = job.JobRequestID,
                                CustomerPrice = 0,
                                CompanyPrice = 0,
                                AgreedPrice = 0
                            };
                        }

                        job.PriceAgreement = priceAgreement;
                    }
                     
                    // Return the processed list of job requests
                    executionResult.SetData(jobRequests);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region CreateJobRequest
        [HttpPost]
        [Route("CreateJobRequest")]

        public async Task<IActionResult> CreateJobRequest([FromBody] RequestWithPaymentPayload newJobRequest)
        {

            var executionResult = new ExecutionResult();
            string functionName = nameof(CreateJobRequest);
          
            JobRequest payload = new JobRequest
            {
                JobRequestID = Functions.GenerateJobRequestId()
            };

            // Create PriceAgreement object
            PriceAgreementController PriceCntrl = new PriceAgreementController(_context);
            PriceAgreementPayload priceDetails = new PriceAgreementPayload
            {
                PriceAgreementID = Functions.GeneratePriceAgreementId(),
                CompanyPrice = newJobRequest.RequestedPrice,
                AgreedPrice = newJobRequest.AcceptedPrice,
                CustomerPrice = newJobRequest.CustomerPrice,
                CompanyID=newJobRequest.CompanyID,
                JobRequestID= payload.JobRequestID,
                CustomerID=newJobRequest.CustomerID
            };
            using (var transaction = _context.Database.BeginTransaction()) // Start transaction
            {
                var priceInsertResult = await PriceCntrl.NewPriceAgreement(priceDetails);
                var priceDataResult = priceInsertResult.GetData();

                // Convert empty string fields to null for nullable fields
                payload.PickupLocation = string.IsNullOrEmpty(newJobRequest.PickupLocation) ? null : newJobRequest.PickupLocation;
                payload.DeliveryLocation = string.IsNullOrEmpty(newJobRequest.DeliveryLocation) ? null : newJobRequest.DeliveryLocation;
                payload.CargoDescription = string.IsNullOrEmpty(newJobRequest.CargoDescription) ? null : newJobRequest.CargoDescription;
                payload.ContainerNumber = string.IsNullOrEmpty(newJobRequest.ContainerNumber) ? null : newJobRequest.ContainerNumber;
                payload.Status = string.IsNullOrEmpty(newJobRequest.Status) ? "CREATED" : newJobRequest.Status;
                payload.TruckType = string.IsNullOrEmpty(newJobRequest.TruckType) ? null : newJobRequest.TruckType;
                payload.TruckID = string.IsNullOrEmpty(newJobRequest.TruckID) ? null : newJobRequest.TruckID;
                payload.DriverID = string.IsNullOrEmpty(newJobRequest.DriverID) ? null : newJobRequest.DriverID;
                payload.RequestType = string.IsNullOrEmpty(newJobRequest.RequestType) ? null : newJobRequest.RequestType;

                payload.CustomerID = string.IsNullOrEmpty(newJobRequest.CustomerID) ? "NOT SET" : newJobRequest.CustomerID;

                // Set PriceAgreementID
                payload.PriceAgreementID = priceDetails.PriceAgreementID;
                payload.Cdate = SysDate;

                try
                {
                    // Save the new JobRequest
                    _context.JobRequests.Add(payload);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    executionResult.SetData(payload);
                    return Ok(executionResult.GetServerResponse());
                }
                catch (Exception ex)
                {
                    executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                    return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
                }
            }
        }

        #endregion

        #region UpdateJobRequest
        [HttpPut]
        [Route("UpdateJobRequest/{jobRequestID}")]
        public async Task<IActionResult> UpdateJobRequestAsync(string jobRequestID, [FromBody] RequestWithPaymentPayload updatedJobRequest)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(UpdateJobRequestAsync);
            


            try
            {
                using (var db = new AppDbContext(_config))
                {
                    using (var transaction = db.Database.BeginTransaction()) // Start transaction
                    {
                        var existingJobRequest = db.JobRequests.FirstOrDefault(jr => jr.JobRequestID == jobRequestID);
                        if (existingJobRequest == null)
                        {
                            return NotFound("Job Request not found");
                        }
                        #region Updating Existing JobRequest 
                        // Update JobRequest fields if provided
                        existingJobRequest.PickupLocation = string.IsNullOrEmpty(updatedJobRequest.PickupLocation) ? existingJobRequest.PickupLocation : updatedJobRequest.PickupLocation;
                        existingJobRequest.DeliveryLocation = string.IsNullOrEmpty(updatedJobRequest.DeliveryLocation) ? existingJobRequest.DeliveryLocation : updatedJobRequest.DeliveryLocation;
                        existingJobRequest.CargoDescription = string.IsNullOrEmpty(updatedJobRequest.CargoDescription) ? existingJobRequest.CargoDescription : updatedJobRequest.CargoDescription;
                        existingJobRequest.ContainerNumber = string.IsNullOrEmpty(updatedJobRequest.ContainerNumber) ? existingJobRequest.ContainerNumber : updatedJobRequest.ContainerNumber;

                        if ((updatedJobRequest.RequestedPrice != 0) && (updatedJobRequest.AcceptedPrice == 0))
                        {
                            updatedJobRequest.Status = "ON AGREEMENT";
                        }
                        if (updatedJobRequest.AcceptedPrice != 0)
                        {
                            updatedJobRequest.Status = "PENDING PAYMENTS";
                            existingJobRequest.AssignedCompany = updatedJobRequest.CompanyID;
                            existingJobRequest.PriceAgreementID = updatedJobRequest.PriceAgreementID;
                        }

                        existingJobRequest.Status = string.IsNullOrEmpty(updatedJobRequest.Status) ? existingJobRequest.Status : updatedJobRequest.Status;


                        existingJobRequest.TruckID = string.IsNullOrEmpty(updatedJobRequest.TruckID) ? existingJobRequest.TruckID : updatedJobRequest.TruckID;
                        existingJobRequest.CustomerID = string.IsNullOrEmpty(updatedJobRequest.CustomerID) ? existingJobRequest.CustomerID : updatedJobRequest.CustomerID;

                        existingJobRequest.Udate = SysDate;

                        #endregion

                        var existingPriceAgreement = db.PriceAgreements.FirstOrDefault(pa => pa.CompanyID == updatedJobRequest.CompanyID && pa.JobRequestID == updatedJobRequest.JobRequestID);
                        if (existingPriceAgreement == null)
                        {
                            PriceAgreementController PriceCntrl = new PriceAgreementController(_context);
                            PriceAgreementPayload priceDetails = new PriceAgreementPayload
                            {
                                PriceAgreementID = Functions.GeneratePriceAgreementId(),
                                CompanyPrice = updatedJobRequest.RequestedPrice,
                                AgreedPrice = updatedJobRequest.AcceptedPrice,
                                CustomerPrice = updatedJobRequest.CustomerPrice,
                                JobRequestID = updatedJobRequest.JobRequestID,
                                CustomerID = updatedJobRequest.CustomerID,
                                CompanyID = updatedJobRequest.CompanyID,
                            };
                            var priceInsertResult = await PriceCntrl.NewPriceAgreement(priceDetails);

                            var CreatedPriceAgreement = db.PriceAgreements.FirstOrDefault(pa => pa.PriceAgreementID == priceDetails.PriceAgreementID);

                            CreatedPriceAgreement.CompanyPrice = updatedJobRequest.RequestedPrice;
                            CreatedPriceAgreement.AgreedPrice = updatedJobRequest.AcceptedPrice;
                            CreatedPriceAgreement.CustomerPrice = updatedJobRequest.CustomerPrice;
                            CreatedPriceAgreement.JobRequestID = updatedJobRequest.JobRequestID;
                            CreatedPriceAgreement.CustomerID = updatedJobRequest.CustomerID;
                            CreatedPriceAgreement.CompanyID = updatedJobRequest.CompanyID;

                        }
                        // Find the associated PriceAgreement using the PriceAgreementID
                        else
                        {

                            // Update PriceAgreement fields if provided
                            if (updatedJobRequest.RequestedPrice.HasValue && updatedJobRequest.RequestedPrice > 0)
                                existingPriceAgreement.CompanyPrice = updatedJobRequest.RequestedPrice.Value;
                            if (updatedJobRequest.AcceptedPrice.HasValue && updatedJobRequest.AcceptedPrice > 0)
                                existingPriceAgreement.AgreedPrice = updatedJobRequest.AcceptedPrice.Value;
                            if (updatedJobRequest.CustomerPrice.HasValue && updatedJobRequest.CustomerPrice > 0)
                                existingPriceAgreement.CustomerPrice = updatedJobRequest.CustomerPrice.Value;
                            existingPriceAgreement.CompanyID = updatedJobRequest.CompanyID;
                            existingPriceAgreement.JobRequestID = updatedJobRequest.JobRequestID;
                            existingPriceAgreement.CustomerID = updatedJobRequest.CustomerID;
                        }

                        // Save changes to both JobRequest and PriceAgreement
                        db.SaveChanges();
                        transaction.Commit();
                        executionResult.SetData(existingJobRequest);
                        return Ok(executionResult.GetServerResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion
         

        #region DeleteJobRequest
        [HttpDelete]
        [Route("DeleteJobRequest/{jobRequestID}")]
        public async Task<IActionResult> DeleteJobRequest(string jobRequestID)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(DeleteJobRequest);

            try
            {
                var jobRequest = await _context.JobRequests.FindAsync(jobRequestID);
                if (jobRequest == null)
                {
                    return NotFound("Job Request not found");
                }

                _context.JobRequests.Remove(jobRequest);
                await _context.SaveChangesAsync();

                executionResult.SetData(jobRequest);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region BillingRequests

        #region GetPendingChargableItems
        [HttpGet]  // Explicit HTTP method binding
        [Route("GetPendingChargableItems")]
        public List<JobRequest> GetPendingChargableItems()
        {
            using (var db = new AppDbContext(_config))
            {
                return db.JobRequests
                    .Include(jr => jr.PriceAgreement)
                    .Include(jr => jr.Truck)
                    .Include(jr => jr.Customer)
                    .Where(jr => jr.Status == "PENDING PAYMENTS")
                    .ToList();
            }
        }
        #endregion


        #region UpdateRequestStatus
        [HttpPut]
        [Route("UpdateRequestStatus/{jobRequestID}")]
        public async Task<IActionResult> UpdateRequestStatus(string jobRequestID, string newStatus)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(UpdateRequestStatus);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var existingJobRequest = db.JobRequests.FirstOrDefault(jr => jr.JobRequestID == jobRequestID);
                    if (existingJobRequest == null)
                    {
                        return NotFound("Job Request not found");
                    }

                    #region Updating Existing JobRequest
                    existingJobRequest.Status = newStatus;
                    existingJobRequest.Udate = DateTime.UtcNow.ToLocalTime();
                    #endregion

                    // Save changes to JobRequest
                    await db.SaveChangesAsync();

                    executionResult.SetData(existingJobRequest);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion
        #endregion
    }
}