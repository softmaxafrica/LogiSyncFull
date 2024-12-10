using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiSyncWebApi.Server.Models;
 using LogiSyncWebApi.Server.Shared;

namespace LogiSyncWebApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public InvoiceController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        #region GetAllInvoices
        [HttpGet]
        [Route("GetAllInvoices")]
        public IActionResult GetAllInvoices()
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetAllInvoices);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var invoices = db.Invoices
                                .Include(i => i.Payments)
                                .Include(c => c.CustomerDetails)
                                .ToList();
                    executionResult.SetData(invoices);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion


        #region GetCompanyInvoice
        [HttpGet]
        [Route("GetCompanyInvoice/{CompanyId}")]
        public IActionResult GetInvoiceById(string CompanyId)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetInvoiceById);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var invoice = db.Invoices
                        .Include(c=> c.CustomerDetails)
                        .Where(i => i.CompanyID == CompanyId).ToList();
                    if (invoice == null)
                    {
                        return NotFound("No Invoice Avilable");
                    }
                    executionResult.SetData(invoice);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region GetCompanyInvoiceDetails
        [HttpGet]
        [Route("GetCompanyInvoiceDetails/{CompanyId}/{InvoiceNumber}")]
        public IActionResult GetCompanyInvoiceDetails(string CompanyId,int InvoiceNumber)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetInvoiceById);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var invoice = db.Invoices
                                     .Include(c => c.CustomerDetails)
                                     .Where(i => i.CompanyID == CompanyId && i.InvoiceNumber==InvoiceNumber).FirstOrDefault();
                    if (invoice == null)
                    {
                        return NotFound("No Invoice Avilable");
                    }
                    executionResult.SetData(invoice);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion


        #region GetInvoiceById
        [HttpGet]
        [Route("GetInvoiceById/{invoiceNumber}")]
        public IActionResult GetInvoiceById(int invoiceNumber)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetInvoiceById);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var invoice = db.Invoices
                        .Include(c => c.CustomerDetails)
                        .FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
                    if (invoice == null)
                    {
                        return NotFound("Invoice not found");
                    }
                    executionResult.SetData(invoice);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region CreateInvoice
        [HttpPost]
        [Route("CreateInvoice")]
        public async Task<Invoice> CreateInvoice([FromBody] Invoice newInvoice)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(CreateInvoice);

           
                _context.Invoices.Add(newInvoice);
                await _context.SaveChangesAsync();

                 return newInvoice;
          
           
        }
        #endregion

        #region UpdateInvoice
        [HttpPut]
        [Route("UpdateInvoice/{invoiceNumber}")]
        public IActionResult UpdateInvoice(int invoiceNumber, [FromBody] Invoice updatedInvoice)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(UpdateInvoice);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var existingInvoice = db.Invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
                    if (existingInvoice == null)
                    {
                        return NotFound("Invoice not found");
                    }

                    existingInvoice.CustomerID = updatedInvoice.CustomerID;
                    existingInvoice.JobRequestID = updatedInvoice.JobRequestID;
                    existingInvoice.ServiceCharge = updatedInvoice.ServiceCharge;
                    existingInvoice.OperationalCharge = updatedInvoice.OperationalCharge;
                    existingInvoice.PaymentId= updatedInvoice.PaymentId;

                    existingInvoice.IssueDate = updatedInvoice.IssueDate;
                    existingInvoice.DueDate = updatedInvoice.DueDate;
                    existingInvoice.Status = updatedInvoice.Status;

                    db.SaveChanges();

                    executionResult.SetData(existingInvoice);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region UpdateInvoicePaymentDetails
        [HttpPut]
        [Route("UpdateInvoicePaymentDetails/{invoiceNumber}")]
        public async Task UpdateInvoicePaymentDetails(int invoiceNumber)
        {
            // Retrieve the invoice with its associated payments
            var invoice = await _context.Invoices
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
            if (invoice == null)
                throw new Exception("Invoice not found.");
            // Sum the total payments for the invoice
            double totalPaid = invoice.Payments?.Sum(p => p.AmountPaid) ?? 0;
            // Determine and set the invoice status
            if (totalPaid == 0)
            {
                invoice.Status = "DRAFT"; // No payments made
            }
            else if (totalPaid < invoice.TotalAmount)
            {
                invoice.Status = "PARTIAL"; // Payments made but not fully paid
            }
            else if (totalPaid >= invoice.TotalAmount)
            {
                invoice.Status = "PAID"; // Fully paid
            }

            // Update financial details
            invoice.TotalPaidAmount = totalPaid;
            invoice.OwedAmount = invoice.TotalAmount - totalPaid;

            // Save the updated invoice status
            await _context.SaveChangesAsync();
        }
        #endregion


        #region DeleteInvoice
        [HttpDelete]
        [Route("DeleteInvoice/{invoiceNumber}")]
        public async Task<IActionResult> DeleteInvoice(int invoiceNumber)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(DeleteInvoice);

            try
            {
                var invoice = await _context.Invoices.FindAsync(invoiceNumber);
                if (invoice == null)
                {
                    return NotFound("Invoice not found");
                }

                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();

                executionResult.SetData(invoice);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(InvoiceController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion
    }
}
