using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiSyncWebApi.Server.Models;
 using System.Threading.Tasks;
using System.Linq;
using LogiSyncWebApi.Server.Shared;

namespace LogiSyncWebApi.Server.Controllers.Billing
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context; 
        private readonly IConfiguration _config;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        #region GetAllPayments
        [HttpGet]
        [Route("GetAllPayments")]
        public IActionResult GetAllPayments()
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetAllPayments);

            try
            {
                var payments = _context.Payments
                    //.Include(p => p.Invoice) // Include navigation property
                    .ToList();
                executionResult.SetData(payments);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(PaymentController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region GetPaymentById
        [HttpGet]
        [Route("GetPaymentById/{id}")]
        public IActionResult GetPaymentById(string id)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetPaymentById);

            try
            {
                var payment = _context.Payments
                    //.Include(p => p.Invoice) // Include navigation property
                    .FirstOrDefault(p => p.PaymentID == id);

                if (payment == null)
                {
                    return NotFound("Payment not found");
                }

                executionResult.SetData(payment);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(PaymentController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

         


         #region AddPayment
        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody] Payment newPayment)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(AddPayment);
            InvoiceController invController = new InvoiceController(_context, _config);


            try
            {
                // Check if the invoice exists
                var invoiceToBePaid = await _context.Invoices
                    .FirstOrDefaultAsync(p => p.InvoiceNumber == newPayment.InvoiceNumber);

                if (invoiceToBePaid == null)
                {
                    return NotFound("Invoice not found! Please provide a valid invoice.");
                }

                // Add the new payment
                await _context.Payments.AddAsync(newPayment);

                // Update the invoice payment details
                await invController.UpdateInvoicePaymentDetails(newPayment.InvoiceNumber);
                // Save all changes to the database
                await _context.SaveChangesAsync();

                // Set the result and return success response
                executionResult.SetData(newPayment);
                return Ok("Payment added successfully.");
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(PaymentController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region UpdatePayment
        [HttpPut]
        [Route("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(string id, [FromBody] Payment updatedPayment)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(UpdatePayment);

            try
            {
                var payment = await _context.Payments.FindAsync(id);

                if (payment == null)
                {
                    return NotFound("Payment not found");
                }

                payment.InvoiceNumber = updatedPayment.InvoiceNumber;
                payment.AmountPaid = updatedPayment.AmountPaid;
                payment.PaymentDate = updatedPayment.PaymentDate;
                payment.PaymentMethod = updatedPayment.PaymentMethod;
                payment.ReferenceNumber = updatedPayment.ReferenceNumber;

                await _context.SaveChangesAsync();

                executionResult.SetData(payment);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(PaymentController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region DeletePayment
        [HttpDelete]
        [Route("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(string id)
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(DeletePayment);

            try
            {
                var payment = await _context.Payments.FindAsync(id);

                if (payment == null)
                {
                    return NotFound("Payment not found");
                }

                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();

                executionResult.SetData(payment);
                return Ok(executionResult.GetServerResponse());
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(PaymentController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion
    }
}
