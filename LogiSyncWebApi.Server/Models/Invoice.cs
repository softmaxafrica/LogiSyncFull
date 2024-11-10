using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogiSyncWebApi.Server.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("INVOICE_NUMBER")]
        public int InvoiceNumber { get; set; }

        [Required]
        [Column("CUSTOMER_ID")]
        public string CustomerID { get; set; }

         [Column("JOB_REQUEST_ID")]
        public string? JobRequestID { get; set; }

        [ForeignKey("JobRequestID")]
        public virtual JobRequest JobRequest { get; set; } // Navigation property

        [Required]
        [Column("AMOUNT")]
        public double Amount { get; set; }

        [Required]
        [Column("ISSUE_DATE")]
        public DateTime IssueDate { get; set; }

        [Column("DUE_DATE")]
        public DateTime? DueDate { get; set; }

        [Required]
        [Column("STATUS")]
        public string Status { get; set; } // e.g., "Pending", "Paid", "Overdue","Cancelled"

        // Navigation properties
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

}
