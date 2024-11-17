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
