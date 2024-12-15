import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DriverPayload } from '../../../models/drivers'; // Adjust the import based on your actual path
import { DataService } from '../../../services/DataService'; // Adjust the import based on your actual path
import { AuthService } from '../../../auth/auth.service'; // Adjust the import based on your actual path
import { GlobalColumnControlService } from '../../../services/ColumnControls';
import { FunctionsService } from '../../../services/functions.service';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { AppConstants } from '../../../services/shared/Constants';
import { TruckType } from '../../../models/TruckTypes';

@Component({
  selector: 'app-driver-vetting',
  templateUrl: './listing.component.html',
  styleUrls: ['../../../app.component.css'],
  providers: [DataService]
})
export class DriverListingComponent implements OnInit {

 
  drivers: DriverPayload[] = [];
  companyId!: string;
  driverForm!: FormGroup;
  displayDialog: boolean = false;
  selectedDriver: DriverPayload | null = null;
  action: 'approve' | 'reject' | null = null;
  columnDialogVisible: boolean = false;
  first: number = 0;
  rows: number = 10;
  editDialogVisible: boolean = false;
  deleteDialogVisible: boolean = false;
  actionMenu: MenuItem[] | null | undefined;
  public baseUrl = AppConstants.API_SERVER_URL;

  assignTruckDialogVisible: boolean = false;
  assignTruckForm!: FormGroup;
  availableTruckTypes: { label: string, value: string }[] = [];
  selectedDriverForAssignment: DriverPayload | null = null;
  

  constructor(
    private dataService: DataService,
    private authService: AuthService,
    private fb: FormBuilder,
    public globalColumnControlService: GlobalColumnControlService,
    public functions: FunctionsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeColumns();
    this.companyId = this.authService.getCompanyId() || '';
    this.getCompanyDrivers();
    this.loadActionMenu();
    this.initializeAssignTruckForm();

    this.editDialogVisible = false;

    
    // Initialize the form with validations
    this.driverForm = this.fb.group({
      fullName: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      companyID: [{ value: '', disabled: true }, Validators.required],
      status: [{ value: '', disabled: true }, Validators.required],
      licenseClasses: [''],
      licenseExpireDate: ['', Validators.required], // License Expire Date should not be empty
      isAvilableForBooking: [false],
      registrationComment: ['']
    });
  }

  initializeAssignTruckForm(): void {
    this.assignTruckForm = this.fb.group({
      truckTypes: [[], Validators.required],
    });
  }
  loadActionMenu() {
    this.actionMenu = [
      {
        label: 'All',
        icon: 'assets/images/icons/test-drive.png',
        command: () => {
          this.openNewJobDialog();
          this.router.navigate(['/home/drivers/listing']);
        }
      },
      {
        label: 'Add',
        icon: 'assets/images/icons/add.png',
        command: () => {
          this.openNewJobDialog();
          this.router.navigate(['/home/drivers/registration']);
        }
      },
      {
        label: 'Vetting',
        icon: 'assets/images/icons/endorsement.png',
        command: () => {
          this.openNewJobDialog();
          this.router.navigate(['/home/drivers/vetting']);
        }
      },

    ];
  }

  openNewJobDialog() {
    // Placeholder method for opening a new job dialog
  }

  initializeColumns() {
    this.globalColumnControlService.setSourceColumns([
      { field: 'companyID', header: 'Company ID' },
      { field: 'driverID', header: 'Driver ID' },
      { field: 'licenseNumber', header: 'License Number' },
      { field: 'licenseClasses', header: 'License Classes' },
      { field: 'isAvilableForBooking', header: 'Is Driver Available' }
    ]);

    this.globalColumnControlService.setTargetColumns([
      { field: 'imageUrl', header: 'Picture' },
      { field: 'fullName', header: 'Full Name' },
      { field: 'email', header: 'Email' },
      { field: 'status', header: 'Status' },
      { field: 'licenseExpireDate', header: 'License Expire Date' },
      { field: 'phone', header: 'Phone' }
    ]);
  }

  populateForm(driver: DriverPayload): void {
    this.driverForm.setValue({
      fullName: driver.fullName,
      licenseNumber: driver.licenseNumber,
      email: driver.email,
      phone: driver.phone,
      companyID: driver.companyID,
      status: driver.status,
      licenseClasses: driver.licenseClasses,
      // Properly format the date for the form
      licenseExpireDate: driver.licenseExpireDate
        ? new Date(driver.licenseExpireDate).toISOString().split('T')[0]
        : '',
      isAvilableForBooking: driver.isAvilableForBooking,
      registrationComment: '' // Reset the comment field
    });
  }

  isLastPage(): boolean {
    return this.drivers.length
      ? this.globalColumnControlService.first >=
          this.drivers.length - this.globalColumnControlService.rows
      : true;
  }

  onCancel(): void {
    this.displayDialog = false;
    this.functions.displayCancelSuccess(); // Use FunctionsService for cancellation notification
  }

  editDriver(driver: DriverPayload): void {
    this.selectedDriver = driver;
    this.populateForm(driver);
    this.editDialogVisible = true;
  }

  saveDriver(): void {
    if (this.driverForm.valid) {
      const updatedDriver = { ...this.selectedDriver, ...this.driverForm.value };

    const licenseExpireDate = this.driverForm.get('licenseExpireDate')?.value;
    updatedDriver.licenseExpireDate = this.functions.getFormatApiDateOnly(licenseExpireDate);

      updatedDriver.registrationComment = 'NO COMMENT'; // Set default comment


      this.dataService.updateDriver(updatedDriver).subscribe({
        next: (response: any) => {
          this.functions.displayUpdateSuccess();
            
        },
        error: (err) => {
          this.functions.displayError('Failed to update truck status: ' + err.message);
        }
      });
       this.ngOnInit();
    }
 
    }
   



  confirmDelete(driver: DriverPayload): void {
    this.selectedDriver = driver;
    this.deleteDialogVisible = true;
  }

  deleteDriver(): void {
    if (this.selectedDriver) {
      this.dataService.deleteDriver(this.selectedDriver.driverID).subscribe(
        () => {
          this.getCompanyDrivers();
          this.functions.displayDeleteSuccess(); 
          this.deleteDialogVisible = false;
        },
        (error) => {
          const errorMessage = error.error || error.message || 'An unknown error occurred';
          this.functions.displayError(errorMessage); // Use FunctionsService for error notification
        }
      );
    }
  }

  getCompanyDrivers(): void {
    this.dataService.getCompanyDrivers(this.companyId).subscribe(
      (response) => {
        this.drivers = response.data || [];
      },
      (error) => {
        const errorMessage = error.error || error.message || 'An unknown error occurred';
        this.functions.displayError(errorMessage); // Use FunctionsService for error notification
      }
    );
  }
  assignTruckTypes(): void {
    if (this.assignTruckForm.valid && this.selectedDriverForAssignment) {
      const selectedTruckTypes = this.assignTruckForm.value.truckTypes;
      const assignedTruckTypes = selectedTruckTypes.map((truckType: { value: string }) => truckType.value);
      const driverId = this.selectedDriverForAssignment.driverID;
  
      // Send the truckTypes directly as an array (not wrapped in an object)
      this.dataService.assignTruckTypesToDriver(driverId, assignedTruckTypes).subscribe(
        () => {
          this.assignTruckDialogVisible = false;
          this.functions.displaySuccess("Truck types assigned to");
          this.getCompanyDrivers(); // Reload drivers to reflect updates
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
  }
  
  
  
  

  fetchTruckTypes(): void {

    this.dataService.GetTruckTypes().subscribe(
      (response) => {
        this.availableTruckTypes = response.data.map((type: TruckType) => ({
          label: type.typeName, // Display text
          value: type.truckTypeID // Value submitted
      }));      },
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

 
openAssignTruckDialog(driver: DriverPayload): void {
  this.selectedDriverForAssignment = driver;
  this.fetchTruckTypes(); // Ensure this is called before showing the dialog
  this.assignTruckForm.reset();
  this.assignTruckDialogVisible = true;
}

ResendDriverRegistration() {
  if (this.driverForm.valid) {
    const driverToReRegister = { ...this.selectedDriver, ...this.driverForm.value };

  const licenseExpireDate = this.driverForm.get('licenseExpireDate')?.value;
  driverToReRegister.licenseExpireDate = this.functions.getFormatApiDateOnly(licenseExpireDate);

  driverToReRegister.registrationComment = this.selectedDriver?.registrationComment ?? 'RE-REGISTRATION'; // Set default comment
  driverToReRegister.status='PENDING';

    this.dataService.updateDriver(driverToReRegister).subscribe({
      next: (response: any) => {
        this.functions.displaySuccess(response.message);
          
      },
      error: (err) => {
        this.functions.displayError('Failed to update truck status: ' + err.message);
      }
    });
     this.functions.reloadPage();
  }

}

}
