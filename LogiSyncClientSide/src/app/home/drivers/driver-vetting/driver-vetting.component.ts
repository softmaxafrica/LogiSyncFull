import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DriverPayload } from '../../../models/drivers'; // Adjust the import based on your actual path
import { DataService } from '../../../services/DataService'; // Adjust the import based on your actual path
import { AuthService } from '../../../auth/auth.service'; // Adjust the import based on your actual path
import { ApprovalPayload } from '../../../models/ApprovalPayload'; // Adjust the import based on your actual path
import { MenuItem, MessageService } from 'primeng/api'; // Import MessageService for feedback
import { GlobalColumnControlService } from '../../../services/ColumnControls';
import { TablePageEvent } from 'primeng/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-driver-vetting',
  templateUrl: './driver-vetting.component.html',
  styleUrls: ['./driver-vetting.component.css'], // Include your CSS file if needed
  providers: [DataService, MessageService] // Provide MessageService for feedback
})
export class DriverVettingComponent implements OnInit {
 
 
  drivers: DriverPayload[] = [];
  companyId!: string;
  driverForm!: FormGroup;
  displayDialog: boolean = false;
  selectedDriver: DriverPayload | null = null;
  action: 'approve' | 'reject' | null = null;
  columnDialogVisible: boolean = false;
  first: number = 0;
  rows: number = 10;

  actionMenu: MenuItem[] | null | undefined;

  constructor(
    private dataService: DataService,
    private authService: AuthService,
    private fb: FormBuilder,
    private messageService: MessageService,
    public globalColumnControlService: GlobalColumnControlService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initializeColumns();
    this.loadActionMenu();
    this.companyId = this.authService.getCompanyId() || '';
    this.loadDriversAwaitingApproval();

    this.driverForm = this.fb.group({
      fullName: [{ value: '', disabled: true }],
      licenseNumber: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: true }],
      phone: [{ value: '', disabled: true }],
      registrationComment: [''],
      companyID: [{ value: this.companyId, disabled: true }]
    });
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
        label: 'Add',
        icon: 'assets/images/icons/add.png',
        command: () => {
           this.router.navigate(['/home/drivers/registration']);
        }
      },

    ];
  }
  initializeColumns() {
    this.globalColumnControlService.setSourceColumns([
      { field: 'companyID', header: 'Company ID' },
      { field: 'licenseNumber', header: 'License Number' },
    ]);

    this.globalColumnControlService.setTargetColumns([
      { field: 'driverID', header: 'Driver Id' },
      { field: 'fullName', header: 'Full Name' },
      { field: 'email', header: 'Email' },
      { field: 'status', header: 'Status' },
      { field: 'phone', header: 'Phone' },


    ]);
  }

  loadDriversAwaitingApproval(): void {
    this.dataService.getDriversAwaitingApproval(this.companyId).subscribe(
      (response) => {
        this.drivers = response.data;
      },
      (error) => {
        console.error('Error fetching drivers:', error);
      }
    );
  }

  approveDriver(driver: DriverPayload): void {
    this.selectedDriver = driver;
    this.action = 'approve';
    this.populateForm(driver);
    this.displayDialog = true;
  }

  rejectDriver(driver: DriverPayload): void {
    this.selectedDriver = driver;
    this.action = 'reject';
    this.populateForm(driver);
    this.displayDialog = true;
  }

  populateForm(driver: DriverPayload): void {
    this.driverForm.setValue({
      fullName: driver.fullName,
      licenseNumber: driver.licenseNumber,
      email: driver.email,
      phone: driver.phone,
      registrationComment: '', // Reset the comment field
      companyID: this.companyId
    });
    this.driverForm.get('registrationComment')?.enable(); // Enable remarks field
  }

  onSubmit(): void {
    if (this.driverForm.invalid) {
      this.messageService.add({ severity: 'warn', summary: 'Validation Error', detail: 'Please complete all required fields.' });
      return;
    }

    const approvalPayload: ApprovalPayload = {
      userID: this.selectedDriver?.driverID || '',
      status: this.action === 'reject' ? 'REJECTED' : 'APPROVED',
      registrationComment: this.driverForm.value.registrationComment
    };

    this.dataService.approveDriver(approvalPayload).subscribe(
      (response) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: `Driver ${this.action}ed successfully!` });
        this.displayDialog = false;
        this.loadDriversAwaitingApproval();
      },
      (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: `Error ${this.action}ing driver!` });
        console.error('Error approving/rejecting driver:', error);
      }
    );
  }



  isLastPage(): boolean {
    return this.drivers.length ? this.globalColumnControlService.first >= (this.drivers.length - this.globalColumnControlService.rows) : true;
  }
  
  onCancel(): void {
    this.displayDialog = false;
  }

  
}
