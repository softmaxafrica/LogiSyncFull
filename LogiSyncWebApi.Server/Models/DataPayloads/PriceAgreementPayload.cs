using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogiSyncWebApi.Server.Models.DataPayloads
{
    public class PriceAgreementPayload
    {

        public string? PriceAgreementID { get; set; }
        public string? CompanyID { get; set; }
        public string? CustomerID { get; set; }
        public string? JobRequestID { get; set; }
        public decimal? CompanyPrice { get; set; }
        public decimal? CustomerPrice { get; set; }
        public decimal? AgreedPrice { get; set; }
    }

    public class RequestWithPaymentPayload
    {
        public string? JobRequestID { get; set; }
        public string? PickupLocation { get; set; }
        public string? DeliveryLocation { get; set; }
        public string? CargoDescription { get; set; }
        public string? ContainerNumber { get; set; }
        public string? Status { get; set; }
        public string? PriceAgreementID { get; set; }
        public string? TruckType { get; set; }
        public string? TruckID { get; set; }
        public string? DriverID { get; set; }
        public string? RequestType { get; set; } //Truck, Driver, Both
        public string? CustomerID { get; set; }
        public decimal? RequestedPrice { get; set; }
        public decimal? CustomerPrice { get; set; }
        public decimal? AcceptedPrice { get; set; }
    }

}
