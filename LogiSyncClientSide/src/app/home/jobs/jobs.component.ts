import { Component, OnInit } from '@angular/core';
import { JobRequest } from '../../models/jobRequest';
import { DataService } from '../../services/DataService';
import { FunctionsService } from '../../services/functions.service';
import { AuthService } from '../../auth/auth.service';
import { HttpResponse } from '@angular/common/http';
import { TableColumn } from '../../models/table-columns';
import { TablePageEvent } from 'primeng/table';
import { GlobalColumnControlService } from '../../services/ColumnControls';
import { JobRequestPayload, RequestWithPrice } from '../../models/jobRequestPayload';
import { Customer } from '../../models/customer';
import { PriceAgreement } from '../../models/priceAgreement';
import { TrucksComponent } from '../trucks/trucks.component';
import { TruckType } from '../../models/TruckTypes';
import { Truck } from '../../models/Truck';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { AppConstants } from '../../services/shared/Constants';
import { DriverModule } from '../drivers/driver.module';
import { DriverPayload } from '../../models/drivers';
import { ChangeDetectorRef } from '@angular/core';
import { Contract } from '../../models/contract';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['../../app.component.css']
})
export class JobsComponent implements OnInit {
  RequestToDelete: any;



 
  deleteDialogVisible: boolean=false;
 
activeCustomerPrice: any;
activeCustomerAdvanceAmount:any;
activeCompanyAdvanceAmount: any;
activeFinalRequestPrice: any;
activeReqPrice:any;


  contractDetails: Contract | null= null;
  avilableTruckLists: Truck[]=[];
  avilableDriverLists: DriverPayload[]=[];
  SelectedTruckavilableTruck: any;
  showTruckContent: boolean=false;
  truckCompanyDriverFullName: any;
  truckCompanyName: any;
  truckCompanyAdminName: any;
  truckCompanyAdminContact: any;
  truckCompanyDriverContact: any;
  actionMenu: MenuItem[]|null|undefined;
  requestType: any = [];
   

 
  // jobRequests: any;
  jobRequests: JobRequest[] = [];
  selectedJobRequest: JobRequest | null = null;
  jobDialogVisible: boolean = false;
  columnDialogVisible: boolean = false;
  agreementDialogVisible: boolean = false; 

  activeRequest: RequestWithPrice = {
    jobRequestID: '',
    pickupLocation: '',
    deliveryLocation: '',
    cargoDescription: '',
    containerNumber: '',
    status: '',
    priceAgreementID: '',
    truckType: '',
    truckID: '',
    driverID: '',
    requestType: '',
    customerID: '',
    requestedPrice: undefined,  // Set as undefined
    acceptedPrice: undefined,
    customerPrice: undefined,
    companyID:'',
    contractId:'',
    firstDepositAmount: undefined
  };

   
    
    newJobRequest: RequestWithPrice = {
    jobRequestID: '',            
    pickupLocation: '',         
    deliveryLocation: '',       
    cargoDescription: '',      
    containerNumber: '', 
    status: '',                 
    priceAgreementID: '',        
    truckID: '',                
    customerID: '', 
    requestedPrice: 0,      
    acceptedPrice: 0,       
    customerPrice: 0,
    truckType: '',                        
    driverID: '',                              
    requestType: '',
    companyID:'',
    contractId:'',
    invoiceNumber:undefined     
  };

  searchTerm: string = '';
  companyId!: string;
  customersList: Customer[] = [];
  request: JobRequest|undefined;
  rows: number|undefined;
  first: number|null|undefined;

sourceColumns: TableColumn[] = []; // Available columns
targetColumns: TableColumn[] = []; // Selected columns
 truckTypes: TruckType[] = [];
 selectedTruckTypeID: any;
 selectedTruckDescription: any;
requestDetailsVisible: boolean =false;
  ActiveCustomerName: string | undefined;
  isLoading: boolean= false;
  showDriverContent: boolean= false;
showPriceContent: boolean= true;
showPriceLabels: boolean=false;

  filteredJobs: JobRequest[]=[];
  PriceDetails: PriceAgreement | null = null;
payment: any;
 
   constructor(
     private dataServices: DataService,
    public functions: FunctionsService,
    private authServices: AuthService,
    public globalColumnControlService: GlobalColumnControlService,
    private dataService: DataService, 
    private changeDetector: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit() {
     this.companyId = this.authServices.getCompanyId() || '';
    this.loadJobs(this.companyId);
    this.loadColumns();
    this.loadCustomers();
    this.getTruckTypes();
    this.loadActionMenu();
    this.preLoads();
    this.filteredJobs=this.jobRequests;
   }
  preLoads() {
    this.requestType = [
      { label: 'TRUCK ONLY', value: 'TRUCK' },
      { label: 'DRIVER ONLY', value: 'DRIVER' },
      { label: 'TRUCK WITH DRIVER', value: 'TRUCK WITH DRIVER' }

    ];  }
  loadCustomers() {
       this.dataServices.GetCustomersByCompany(this.companyId).subscribe(
        (response) => this.customersList = response.data,
        (error) => {
          const errorMessage =
          error.error?.message ||  
          (typeof error.error === 'string' ? error.error : null) ||  
          error.message || // General HTTP error message
          'An unknown error occurred';
        this.functions.displayError(errorMessage);
        }      );
    }    
    loadContractDetails(ContrId: string) {
      if (ContrId) {
        this.dataService.getContractById(this.activeRequest.contractId).subscribe(
          (response)  => this.contractDetails = response.data,
          (error) => {
            const errorMessage =
            error.error?.message ||  
            (typeof error.error === 'string' ? error.error : null) ||  
            error.message || // General HTTP error message
            'An unknown error occurred';
          this.functions.displayError(errorMessage);
          }         );
      }
    }
    loadPriceDetails(priceAgreementID: string) {
      if (priceAgreementID) {
        this.dataService.getPriceDataById(this.activeRequest.priceAgreementID).subscribe(
          (response)  => this.PriceDetails = response.data,
          (error) => {
            const errorMessage =
            error.error?.message ||  
            (typeof error.error === 'string' ? error.error : null) ||  
            error.message || // General HTTP error message
            'An unknown error occurred';
          this.functions.displayError(errorMessage);
          }         );
      }
    }
loadActionMenu(){
  this.actionMenu = [
    {
      label: 'add',
      icon: 'assets/images/icons/add.png',   
      command: () => {
        this.openNewJobDialog();
      }
    },
    {
      label: 'edit',
      icon: 'assets/images/icons/edit.png',   
      command: () => {
        console.log('Edit action triggered');
        // this.router.navigate(['/edit-item']);
      }
    },
    {
      label: 'delete',
      icon: 'assets/images/icons/delete.png',   
      command: () => {
        console.log('Delete action triggered');
        // this.confirmDelete();
      }
    },
    {
      label: 'info',
      icon: 'assets/images/icons/more-info.png',   
      command: () => {
        console.log('Info action triggered');
        // this.router.navigate(['/info']);
      }
    }
  ];
  
}
 
DeleteRequest(request: JobRequest): void {
    this.selectedJobRequest = request;
    this.deleteDialogVisible = true;
    this.RequestToDelete= request.jobRequestID;
  }

  
    confirmDelete(): void {
      
      this.dataServices.DeleteJobRequest(this.RequestToDelete).subscribe(
        (response: any) => {
          if (response && response.success) {
            // this.payments = this.payments.filter((p) => p.paymentID !== paymentID);
            this.functions.displaySuccess('Job Request deleted successfully');
            this.reloadPage();
            this.functions.displayDeleteSuccess();
           } else {
            this.functions.displayInfo('Received an unexpected response format');
          }
        },
        (error) => {
          const errorMessage =
            error.error?.message ||  
            (typeof error.error === 'string' ? error.error : null) ||  
            error.message || // General HTTP error message
            'An unknown error occurred';
          this.functions.displayError(errorMessage);
        }
      );
    }
    
    
isFormValid(): boolean {
  return this.newJobRequest.requestType && 
         this.selectedTruckTypeID && 
         this.newJobRequest.customerID && 
         this.newJobRequest.pickupLocation && 
         this.newJobRequest.deliveryLocation;
}
 
getCustomerName(customerID: string | undefined): string {
  const customerOnRequest = this.customersList.find(cust =>  customerID === customerID); // Ensure to use a property like 'id' for comparison
  this.ActiveCustomerName = customerOnRequest ? customerOnRequest.fullName : undefined; // Assuming 'name' is the property you want
  return this.ActiveCustomerName || 'N/A'; // Return the customer's name or 'N/A'
}

loadJobs(companyId: string): void {
  this.isLoading = true;
  this.dataService.getCompanyJobs(companyId).subscribe(
    (response) => {
      this.isLoading = false;
      if (response.data) {
        this.jobRequests = response.data;
        this.filteredJobs=this.jobRequests;

 console.log(response.data);
      } else {
        this.functions.displayError('No job requests found.');
      }
    },
    (error) => {
      const errorMessage =
      error.error?.message ||  
      (typeof error.error === 'string' ? error.error : null) ||  
      error.message || // General HTTP error message
      'An unknown error occurred';
    this.functions.displayError(errorMessage);
    }
  );

} 
  
  
    // Fetch available truck types
    public  getTruckTypes(): void {
      this.dataService.GetTruckTypes().subscribe(
        (response) => {
          this.truckTypes = response.data;
        },
        (error) => {
          const errorMessage =
          error.error?.message ||  
          (typeof error.error === 'string' ? error.error : null) ||  
          error.message || // General HTTP error message
          'An unknown error occurred';
        this.functions.displayInfo(errorMessage);
        }
      );
    }
  openNewJobDialog() {
     this.jobDialogVisible = true;
  }

  // editJob(job: JobRequest) {
  //   this.newJobRequest = { ...job }; // Clone the job for editing
  //   this.jobDialogVisible = true;
  // }

  saveJob(job: RequestWithPrice) {
    
    this.newJobRequest = { ...job };
this.newJobRequest.companyID=this.companyId;
    this.newJobRequest.requestedPrice = this.newJobRequest.requestedPrice || 0.0;
  this.newJobRequest.acceptedPrice = this.newJobRequest.acceptedPrice || 0.0;
  this.newJobRequest.customerPrice = this.newJobRequest.customerPrice || 0.0;

     if (this.newJobRequest) {
      if (this.newJobRequest.jobRequestID) {
        // Update existing job
       
      } else {
        // Create new 
        //request type
        this.newJobRequest.jobRequestID='JOBREQID';
        this.newJobRequest.status='CREATED';
        this.newJobRequest.requestedPrice=0.00;
        this.newJobRequest.customerPrice=0.00;
        this.newJobRequest.truckType=this.selectedTruckTypeID;
        // this.newJobRequest.truckID=this.SelectedTruckavilableTruck;
        this.newJobRequest.truckID=this.SelectedTruckavilableTruck;
        this.dataServices.createJobRequest('CreateJobRequest',this.newJobRequest).subscribe(() => {
        this.loadJobs(this.companyId); // Refresh job list
        this.functions.displayInsertSuccess();
        this.jobDialogVisible = false;
        }, error => {
          const errorMessage = error.error || error.message || 'An unknown error occurred';
          this.functions.displayError(errorMessage);
        });
      }
    }
  }
  updateRequest( ) {
 
   
   this.activeRequest.companyID=this.companyId;
      this.dataServices.updateJobRequest( this.activeRequest!).subscribe(() => {
       // Refresh job list
      this.jobDialogVisible = false;
    }, error => {
      const errorMessage = error.error || error.message || 'An unknown error occurred';
      this.functions.displayError(errorMessage);
    });
    this.jobDialogVisible=false;
    this.reloadPage();
   }
  
   closeJobDialog() {
     
    this.reloadPage();
    this.getAvailableTrucks('');

     }

 

  editRequests(event: JobRequest) {
      this.showTruckContent=false;
      this.showDriverContent=false;
      
    const selectedJobRequest = event;  // Assuming event.data contains JobRequest
     this.activeReqPrice= event.priceDetails?.companyPrice;
      const ActiveReq: RequestWithPrice = {
      jobRequestID: selectedJobRequest.jobRequestID,
      pickupLocation: selectedJobRequest.pickupLocation,
      deliveryLocation: selectedJobRequest.deliveryLocation,
      cargoDescription: selectedJobRequest.cargoDescription,
      containerNumber: selectedJobRequest.containerNumber,
      status: selectedJobRequest.status,
      priceAgreementID: selectedJobRequest.priceAgreementID,
      truckType: selectedJobRequest.truckType || '',  
      truckID: selectedJobRequest.truckID || '',
      driverID: selectedJobRequest.driverID || '',  // Set this if you can get driverID from the JobRequest
      requestType: selectedJobRequest.requestType ||'',  // Set this if available
      customerID: selectedJobRequest.customerID,
      requestedPrice: selectedJobRequest.priceDetails?.companyPrice ||0 ,  // Use defaults or extract from a relevant property
      acceptedPrice: selectedJobRequest.priceDetails?.agreedPrice ||0,   // Default or extracted value
      customerPrice: selectedJobRequest.priceDetails?.customerPrice||0, 
      companyID: this.companyId,
      contractId: selectedJobRequest.contractId || 'N/A',
      companyAdvanceAmountRequred: selectedJobRequest.companyAdvanceAmountRequred || 0,
      firstDepositAmount: selectedJobRequest.firstDepositAmount || 0

    };
    // Set active request
    this.activeRequest = { ...ActiveReq };
     if(ActiveReq.requestType.includes(AppConstants.TRUCK_REQUEST_TYPE))
    {
      this.getAvailableTrucks(this.activeRequest.truckType);
      this.showTruckContent=true;
    }
 
    if(ActiveReq.contractId !=null){
       this.loadContractDetails(ActiveReq.contractId);
    }
        
     
      if(this.activeRequest.requestType.includes(AppConstants.DRIVER_REQUEST_TYPE)){
        this.getAvailableDrivers(this.companyId);
        this.showDriverContent=true;
      }

      if((ActiveReq.status =="CREATED")||(ActiveReq.status=="ON AGREEMENT") || (ActiveReq.status=='DRAFT'))
      {
        this.showDriverContent=false;
        this.showTruckContent=false;
      }
      if((ActiveReq.status =="READY FOR INVOICE")|| (ActiveReq.status =="READY TO SERVE")||
      (ActiveReq.status=="CANCELLED") || (ActiveReq.status.includes("PENDING"))|| 
      (ActiveReq.status.includes("DRAFT"))  || (ActiveReq.status.includes("INCOMPLETE ADVANCE PAYMENT")))
        {
          this.loadPriceDetails(ActiveReq.priceAgreementID);
          this.showPriceContent=false;
          this.showPriceLabels=true;
        }
    
        this.requestDetailsVisible = true;

  }
  

  viewAgreementDocument(job: JobRequest) {
    this.selectedJobRequest = job;
    this.agreementDialogVisible = true;
  }

  downloadAgreement() {
    if (this.selectedJobRequest?.priceAgreementID) {
  //     this.dataServices.downloadAgreement(this.selectedJobRequest.priceAgreementID).subscribe(data => {
  //       const blob = new Blob([data], { type: 'application/pdf' });
  //       const url = window.URL.createObjectURL(blob);
  //       window.open(url);
  //     }, error => {
  //       const errorMessage = error.error || error.message || 'An unknown error occurred';
  //       this.functions.displayError(errorMessage);
  //     });
  //   }
    }
}

onTruckTypeChange() {
   const selectedTruckType = this.truckTypes.find(truck => truck.truckTypeID === this.selectedTruckTypeID);
  
   if (selectedTruckType) {
    this.selectedTruckDescription = selectedTruckType.description;
  } else {
    this.selectedTruckDescription = null;
  }

  this.getAvailableTrucks(this.selectedTruckTypeID);
  // this.showTruckContent=true;
}
 
   
onTruckChange() {
  const selectedTruck = this.avilableTruckLists.find(
    (truck) => truck.truckID === this.SelectedTruckavilableTruck
  );

  if (selectedTruck) {
    // Check if company and driver exist
    this.truckCompanyName = selectedTruck.company?.companyName || 'N/A';
    this.truckCompanyAdminName = selectedTruck.company?.adminFullName || 'N/A';
    this.truckCompanyAdminContact = selectedTruck.company?.adminContact || 'N/A';

    // Check if the truck has a driver
    this.truckCompanyDriverFullName = selectedTruck.driver?.fullName || 'N/A';
    this.truckCompanyDriverContact = selectedTruck.driver?.phone || 'N/A';
  } else {
    // Reset the fields if no truck is selected
    this.truckCompanyName = null;
    this.truckCompanyAdminName = null;
    this.truckCompanyAdminContact = null;
    this.truckCompanyDriverFullName = null;
    this.truckCompanyDriverContact = null;
  }
}

getAvailableTrucks(type: any) {
  this.dataService.getAvailableTrucks(type, this.companyId).subscribe(
    (response) => {
      this.avilableTruckLists = response.data;  
      console.log(this.avilableTruckLists);
    },
    (error) => {
      const errorMessage =
      error.error?.message ||  
      (typeof error.error === 'string' ? error.error : null) ||  
      error.message || // General HTTP error message
      'An unknown error occurred';
    this.functions.displayInfo(errorMessage);
    }
  );
}


getAvailableDrivers(type: any) {
  this.dataService.getAvilableCompanyDrivers(this.companyId).subscribe(
    (response) => {
      this.avilableDriverLists = response.data;  
     },
    (error) => {
      const errorMessage = error.error?.message || error.message || 'An unknown error occurred'; // Optional chaining for safety
      this.functions.displayError(errorMessage);
    }
  );
}
reloadPage() {
  window.location.reload();
}

//#region  TABLE DATA PREPARE

loadColumns() {
  this.sourceColumns = [
    { field: 'cdate', header: 'Requested Time' },
    { field: 'udate', header: 'Last Update' },
    { field: 'jobRequestID', header: 'Job Request ID' },
    { field: 'invoiceNumber', header: 'Invoice Number'},

    
  

    // TruckDetails (Truck)
    { field: 'truck.model', header: 'Truck Model' },
    { field: 'truck.isTruckAvilableForBooking', header: 'Is Truck Available' },
    { field: 'truckType', header: 'Truck Type' },
    { field: 'truck.truckNumber', header: 'Truck Number' },

    // CustomerDetails (Customer)
    { field: 'customer.email', header: 'Customer Email' },
    { field: 'customer.phone', header: 'Customer Phone' },
    { field: 'customer.address', header: 'Customer Address' },
    { field: 'customer.fullName', header: 'Customer Name' },
  ];

  // Initialize selected columns with default values
  this.targetColumns = [
    { field: 'cargoDescription', header: 'Cargo Description' },
    { field: 'pickupLocation', header: 'Pickup Location' },
    { field: 'deliveryLocation', header: 'Delivery Location' },
    // { field: 'containerNumber', header: 'Reference Number' },
      // PriceDetails (PriceAgreement)
      // { field: 'priceAgreement.priceAgreementID', header: 'Agreement ID' },

      { field: 'priceAgreement.agreedPrice', header: 'Accepted Price' },
      { field: 'priceAgreement.customerPrice', header: 'Customer Price' },
   { field: 'priceAgreement.companyPrice', header: 'Company Price' },

    { field: 'status', header: 'Status' },
  ];
}


getNestedValue(rowData: any, field: string): any {
  return field.split('.').reduce((acc, part) => acc && acc[part], rowData) || null;
}

onSearch() {
  // Extract fields from targetColumns dynamically
  const fieldsToSearch = this.targetColumns.map(column => column.field);

  // Filter the data based on the fields
  this.filteredJobs = this.globalColumnControlService.filterData(this.jobRequests, this.searchTerm, fieldsToSearch);
}
   

//#endregion
}
