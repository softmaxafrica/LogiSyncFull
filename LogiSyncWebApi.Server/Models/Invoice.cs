﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogiSyncWebApi.Server.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("INVOICE_NUMBER")]
        public int InvoiceNumber { get; set; }

        [Column("CUSTOMER_ID")]
        public string CustomerID { get; set; }

        [Column("JOB_REQUEST_ID")]
        public string? JobRequestID { get; set; }

        [Column("PAYMENT_ID")]
        public string? PaymentId { get; set; }

        [Column("TOTAL_AMOUNT")]
        public double TotalAmount { get; set; }

        [Column("SERVICE_CHARGE")]
        public double ServiceCharge { get; set; }

        [Column("OPERATIONAL_CHARGE")]
        public double OperationalCharge { get; set; }

        [Column("ISSUE_DATE")]
        public DateTime IssueDate { get; set; }

        [Column("DUE_DATE")]
        public DateTime? DueDate { get; set; }

        [Column("STATUS")]
        public string Status { get; set; } // e.g., "Pending", "Paid", "Overdue","Cancelled"

        //    // Navigation properties
        //    [ForeignKey("CustomerID")]
        //    public virtual Customer Customer { get; set; }
        //    public virtual ICollection<Payment> Payments { get; set; }
        //}

    }
}
