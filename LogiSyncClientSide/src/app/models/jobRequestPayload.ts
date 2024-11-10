import { PriceAgreement } from "./priceAgreement";
 import { TrucksPayload } from "./TrucksPayload";

export interface JobRequestPayload {
    
    jobRequestID: string;            
    pickupLocation: string;         
    deliveryLocation: string;       
    cargoDescription: string;      
    containerNumber: string; 
    status: string;                 
    truckID: string;                
    customerID: string;
   }

   export interface RequestWithPrice{
    jobRequestID: string;            
    pickupLocation: string;         
    deliveryLocation: string;       
    cargoDescription: string;      
    containerNumber: string; 
    status: string;                 
    priceAgreementID: string;
    truckType: string;                        
    truckID: string;  
    driverID: string;                              
    requestType: string;                
    customerID: string;
    requestedPrice: number;       
     
    acceptedPrice: number;        
    customerPrice?: number;
   }