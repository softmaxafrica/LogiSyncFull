using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LogiSyncWebApi.Server.Models
{
    public class RequestWithPayment
    {
        [Key]
        [Column("PRICE_AGREEMENT_ID")]
        public string PriceAgreementID { get; set; }
        
        [Column("COMPANY_ID")]
        public string? CompanyID { get; set; }
        
        [Column("REQUEST_ID")]
        public string? JobRequestID { get; set; }

        [Column("CUSTOMER_ID")]
        public string? CustomerID { get; set; }

        [Column("COMPANY_PRICE")]
        public decimal? CompanyPrice { get; set; }
        [Column("CUSTOMER_PRICE")]
        public decimal? CustomerPrice { get; set; }

        [Column("AGREED_PRICE")]
        public decimal? AgreedPrice { get; set; }
    }

}
