using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogiSyncWebApi.Server.Models
{
    public class Payment
    {
        [Key]
        [Column("PAYMENT_ID")]
        public string PaymentID { get; set; }

        [Required]
        [Column("INVOICE_NUMBER")]
        public int InvoiceNumber { get; set; }

        //[ForeignKey("InvoiceNumber")]
        //public virtual Invoice Invoice { get; set; } // Navigation property

        [Required]
        [Column("AMOUNT_PAID")]
        public double AmountPaid { get; set; }

        [Required]
        [Column("PAYMENT_DATE")]
        public DateTime PaymentDate { get; set; }
        
        [Required]
        [Column("PAYMENT_METHOD")]
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "Bank Transfer"

        [Column("REFERENCE_NUMBER")]
        public string ReferenceNumber { get; set; } // Optional
    }
}
