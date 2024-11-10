export interface DriverPayload {
  driverID: string;
  fullName: string;
  email: string;
  phone: string;
  licenseNumber: string;
  status: string | "ACTIVE";
  registrationComment: string;
  companyID: string;
  licenseClasses: string;                
  licenseExpireDate: Date;               
  isAvilableForBooking: boolean;       
  imageUrl?: string;                     
}
