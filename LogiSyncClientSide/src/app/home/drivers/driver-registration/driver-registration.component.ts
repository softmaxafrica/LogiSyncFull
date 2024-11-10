import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../services/DataService';
import { DriverPayload } from '../../../models/drivers';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';
import { FileUploadEvent, FileUploadHandlerEvent } from 'primeng/fileupload';
import { FunctionsService } from '../../../services/functions.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-driver-registration',
  templateUrl: './driver-registration.component.html',
  styleUrls: ['../../../app.component.css']
})
export class DriverRegistrationComponent implements OnInit {
  actionMenu: MenuItem[] | null | undefined;
onUpload($event: FileUploadEvent) {
throw new Error('Method not implemented.');
}


// selectedFile: File[]=[];
selectedFile: File | null = null;

  driverPayload: DriverPayload = {
    status: 'PENDING', 
    companyID: '',
    driverID : '',              
    fullName : '',              
    email : '',                
    phone : '',                
    licenseNumber : '',         
     registrationComment : '', 
     licenseClasses: '',                
  isAvilableForBooking: true,     
  licenseExpireDate: new Date,  
  imageUrl: '',  
     
   };  
  registrationStatus: { label: string, value: string }[] = [];
  messages: { severity: string, summary: string, detail: string }[] = [];
  selectedStatus: string = 'PENDING';  

  constructor(private dataService: DataService,
               private authService: AuthService,
               private functions :FunctionsService,  
              private router: Router) {}

  ngOnInit(): void {
    this.loadActionMenu();
    this.registrationStatus = [
      { label: 'Pending', value: 'PENDING' },
      { label: 'Approved', value: 'APPROVED' },
      { label: 'Rejected', value: 'REJECTED' }
    ];

    this.driverPayload.companyID = this.authService.getCompanyId() || '';  
    this.driverPayload.driverID = '';  
    this.driverPayload.status = this.selectedStatus;
  }
  onImageSelected(event: any): void {
    this.selectedFile = event.files[0];  // PrimeNG file upload returns an array of files
  }
 
  loadActionMenu() {
    this.actionMenu = [
      {
        label: 'All',
        icon: 'assets/images/icons/test-drive.png',
        command: () => {
           this.router.navigate(['/home/drivers/listing']);
        }
      },
 
      {
        label: 'Vetting',
        icon: 'assets/images/icons/endorsement.png',
        command: () => {
           this.router.navigate(['/home/drivers/vetting']);
        }
      },

    ];
  }

  isFormValid(): boolean {
    // Check if all required fields are non-empty and valid
    return (
      this.driverPayload.fullName?.trim() !== '' &&
      this.driverPayload.email?.trim() !== '' &&
      this.driverPayload.phone?.trim() !== '' &&
      this.driverPayload.licenseNumber?.trim() !== '' &&
      this.driverPayload.companyID?.trim() !== '' &&
      // Add any additional validations as needed
      this.driverPayload.licenseExpireDate instanceof Date && // Checks if it's a valid Date
      this.driverPayload.licenseClasses?.trim() !== '' // Optional but checking if it's filled
    );
  }


  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onRegister() {
    if (this.isFormValid()) {
      const formData = new FormData();

      formData.append('FullName', this.driverPayload.fullName);
      formData.append('Email', this.driverPayload.email);
      formData.append('Phone', this.driverPayload.phone);
      formData.append('LicenseNumber', this.driverPayload.licenseNumber);
      formData.append('CompanyID', this.driverPayload.companyID);
      formData.append('LicenseExpireDate', this.driverPayload.licenseExpireDate ? new Date(this.driverPayload.licenseExpireDate).toISOString().split('T')[0] : '');
      formData.append('LicenseClasses', this.driverPayload.licenseClasses || '');
      formData.append('RegstrationComment', this.driverPayload.registrationComment || '');
      formData.append('Status', this.driverPayload.status || '');
      formData.append('isAvilableForBooking', this.driverPayload.isAvilableForBooking.toString());

      // Add the selected file to FormData if it exists
      if (this.selectedFile) {
        formData.append('file', this.selectedFile, this.selectedFile.name);
      }

      // Send the FormData object
      this.dataService.postDriverFormData('RegisterDriver', formData)
        .subscribe({
          next: (response: any) => {
            if (response.success) {
              this.messages = [{ severity: 'success', summary: 'Success', detail: 'Driver registered successfully' }];
              this.router.navigate(['/home/dashboard']);
            } else {
              this.messages = [{ severity: 'error', summary: 'Error', detail: response.message || 'Registration failed. Please try again.' }];
            }
          },
          error: (err: any) => {
            this.functions.displayError(err.error);
            
          }
        });
    } else {
      this.messages = [{ severity: 'warn', summary: 'Warning', detail: 'Please fill out all required fields' }];
    }
  }
}

