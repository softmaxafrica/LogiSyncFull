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
}
