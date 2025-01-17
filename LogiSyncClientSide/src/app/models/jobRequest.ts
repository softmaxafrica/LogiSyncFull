import { Customer } from "./customer";
import { Invoice } from "./invoices";
import { PriceAgreement } from "./priceAgreement";
 import { TrucksPayload } from "./TrucksPayload";

 

export interface JobRequest {
    jobRequestID: string;    
    assignedCompany: string;        
    pickupLocation: string;         
    deliveryLocation: string;       
    cargoDescription: string;       
    containerNumber: string; 
    requestType: string; // Added
    status: string;                 
    priceAgreementID: string;        
    truckID: string;                
    customerID: string;
    truckType: string; // Added
    driverID: string; // Added
    invoiceNumber:number;
    priceDetails: PriceAgreement;
    truckDetails: TrucksPayload;
    customerDetails: Customer;
    invoices: Invoice[]; // Added
    cdate:Date,
    udate :Date,
    contractId:string;
    firstDepositAmount: number;
    companyAdvanceAmountRequred:number;
}
