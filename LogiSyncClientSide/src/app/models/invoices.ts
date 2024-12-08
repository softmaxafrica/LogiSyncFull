import { Customer } from "./customer";
import { JobRequest } from "./jobRequest";
import { Payment } from "./payments";

export interface Invoice {

    invoiceNumber: number;
    companyID: string;            
    customerID: string;           
    jobRequestID?: string;       
    PaymentId:string;             
    totalAmount: number;   
    serviceCharge: number;                
    operationalCharge: number;                
    issueDate: Date;               
    dueDate?: Date;                 
    status: string;                
}
