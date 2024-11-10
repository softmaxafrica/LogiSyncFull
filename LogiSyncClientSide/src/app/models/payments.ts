import { Invoice } from "./invoices";

export interface Payment {
    paymentID: string;            // Corresponds to the PaymentID property
    invoiceNumber: number;        // Corresponds to the InvoiceNumber property
    amountPaid: number;           // Corresponds to the AmountPaid property
    paymentDate: Date;            // Corresponds to the PaymentDate property
    paymentMethod: string;        // Corresponds to the PaymentMethod property
    referenceNumber?: string;     // Optional, corresponds to the ReferenceNumber property
    // invoice?: Invoice;            // Optional navigation property
}
