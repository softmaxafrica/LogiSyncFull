using LogiSyncWebApi.Server.Models;
using LogiSyncWebApi.Server.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogiSyncWebApi.Server.Controllers.Billing
{
    public class ChargableItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public ChargableItemsController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        #region GetAllChargableItemsToBill
        [HttpGet]
        [Route("GetAllChargableItemsToBill")]
        public IActionResult GetAllChargableItemsToBill()
        {
            var executionResult = new ExecutionResult();
            string functionName = nameof(GetAllChargableItemsToBill);

            try
            {
                using (var db = new AppDbContext(_config))
                {
                    var ChargableItems = db.ChargableItems
                        .Include(jr => jr.JobRequest)
                                      .ToList();
                    executionResult.SetData(ChargableItems);
                    return Ok(executionResult.GetServerResponse());
                }
            }
            catch (Exception ex)
            {
                executionResult.SetInternalServerError(nameof(ChargableItemsController), functionName, ex);
                return StatusCode(executionResult.GetStatusCode(), executionResult.GetServerResponse().Message);
            }
        }
        #endregion

        #region AddChargableItem
        [HttpPost("AddChargableItem")]
        public async Task<IActionResult> AddChargableItem([FromBody] ChargableItemPayload payload)
        {
            if (payload == null)
            {
                return BadRequest(new { message = "Invalid payload data." });
            }

            try
            {
                // Map payload to ChargableItem entity
                var item = new ChargableItem
                {
                    JobRequestID = payload.JobRequestID,
                    PriceAgreementID = payload.PriceAgreementID,
                    Status = payload.Status,
                    CustomerID = payload.CustomerID,
                    Amount = (double)payload.Amount,
                    ItemDescription=payload.ItemDescription,
                    IssueDate = DateTime.UtcNow.ToLocalTime(),
                    InvoiceNumber = null // InvoiceNumber will be null by default
                };

                await _context.ChargableItems.AddAsync(item);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Chargable item added successfully.",
                    item
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while adding the item.",
                    error = ex.Message
                });
            }
        }
        #endregion
    }
}
