using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiSyncWebApi.Server.Models; 
using LogiSyncWebApi.Server.Shared;
using LogiSyncWebApi.Server.Models.DataPayloads;
using LogiSyncWebApi.Server.Migrations;

namespace LogiSyncWebApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

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
                    var jobRequests = db.JobRequests
                        .Include(jr => jr.PriceAgreement)
                        .Include(jr => jr.Truck)
                        .Include(jr => jr.Customer)
                        //.Where(jr => jr.Truck.CompanyID == CompanyID)
                        .ToList(); // Fetch all matching records as a list

                    if (jobRequests == null || jobRequests.Count == 0)
                    {
                        return NotFound("No Job Requests Available For This Company");
                    }

                    executionResult.SetData(jobRequests); // Set the list of job requests
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
            };

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

            try
            {
                // Save the new JobRequest
                _context.JobRequests.Add(payload);
                await _context.SaveChangesAsync();

                executionResult.SetData(payload);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(JobRequestController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }

        #endregion

        #region UpdateJobRequest
        [HttpPut]
        [Route("UpdateJobRequest/{jobRequestID}")]
        public IActionResult UpdateJobRequest(string jobRequestID, [FromBody] RequestWithPaymentPayload updatedJobRequest)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(UpdateJobRequest);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var existingJobRequest = db.JobRequests.FirstOrDefault(jr => jr.JobRequestID == jobRequestID);
                    if (existingJobRequest == null)
                    {
                        return NotFound("Job Request not found");
                    }

                    existingJobRequest.PickupLocation = updatedJobRequest.PickupLocation;
                    existingJobRequest.DeliveryLocation = updatedJobRequest.DeliveryLocation;
                    existingJobRequest.CargoDescription = updatedJobRequest.CargoDescription;
                    existingJobRequest.ContainerNumber = updatedJobRequest.ContainerNumber;
                    existingJobRequest.Status = updatedJobRequest.Status;
                    existingJobRequest.PriceAgreementID = updatedJobRequest.PriceAgreementID;
                    existingJobRequest.TruckID = updatedJobRequest.TruckID;
                    existingJobRequest.CustomerID = updatedJobRequest.CustomerID;

                    db.SaveChanges();

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
    }
}
