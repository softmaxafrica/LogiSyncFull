import { CompanyPayload } from "./CompanyPayload";
import { Customer } from "./customer";
import { JobRequest } from "./jobRequest";

export interface Contract {
    contractID: string;
    requestID?: string;
    companyID?: string;
    customerID?: string;
    contractDate?: Date;
    termsAndConditions?: string;
    agreedPrice?: number;
    advancePayment?: number;
    advancePaymentDate?: Date;
    status?: string;
  
    jobRequest?: JobRequest;
    company?: CompanyPayload;
    customer?: Customer;
  }