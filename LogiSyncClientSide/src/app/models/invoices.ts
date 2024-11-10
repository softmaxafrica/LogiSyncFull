import { Customer } from "./customer";
import { JobRequest } from "./jobRequest";
import { Payment } from "./payments";

export interface Invoice {
    invoiceNumber: number;         // Corresponds to the InvoiceNumber property
    customerID: string;            // Corresponds to the CustomerID property
    jobRequestID?: string;         // Optional, corresponds to the JobRequestID property
    amount: number;                // Corresponds to the Amount property
    issueDate: Date;               // Corresponds to the IssueDate property
    dueDate?: Date;                // Optional, corresponds to the DueDate property
    status: string;                // Corresponds to the Status property
    jobRequest?: JobRequest;       // Optional navigation property
    customer?: Customer;           // Optional navigation property
    payments?: Payment[];          // Optional navigation property
}
