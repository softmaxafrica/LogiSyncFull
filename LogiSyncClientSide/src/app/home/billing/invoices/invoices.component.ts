import { ChangeDetectorRef, Component } from '@angular/core';
import { Invoice } from '../../../models/invoices';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';
import { GlobalColumnControlService } from '../../../services/ColumnControls';
import { DataService } from '../../../services/DataService';
import { FunctionsService } from '../../../services/functions.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrl: './invoices.component.css'
})
export class InvoicesComponent {
columnDialogVisible: boolean= false;
getNestedValue(_t23: any,arg1: any) {
throw new Error('Method not implemented.');
}
 
 searchTerm: any;
  filteredInvoices: Invoice[]=[];
  globalSearchService: any;
  sourceColumns: any[] = [];
  targetColumns: any[] = [];

 finalizeInvoice(_t15: Invoice) {
throw new Error('Method not implemented.');
}

discardInvoice(_t15: Invoice) {
throw new Error('Method not implemented.');
}
  companyId!: string;
  invoiceList: Invoice[]=[];
  actionMenu: MenuItem[]|null|undefined;
  generateInvoiceVisible: boolean= false;
  layout: string = 'list';

   
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
   this.loadCompanyInvoices(this.companyId);
   this.loadColumns();
    this.loadActionMenu();

   } 
   loadColumns() {
    // Initialize target columns with default values (these will be the visible columns in the table initially)
    this.targetColumns = [
        { field: 'invoiceNumber', header: 'Invoice Number' },
        { field: 'customerID', header: 'Customer ID' }, // Added missing field
        { field: 'totalAmount', header: 'Total Amount' },
        { field: 'status', header: 'Status' },
    ];

    // Source columns include optional and less frequently displayed fields
    this.sourceColumns = [
        { field: 'companyID', header: 'Company ID' }, // Added missing field
        { field: 'jobRequestID', header: 'Job Request ID' }, // Added missing field
        { field: 'paymentId', header: 'Payment ID' },
        { field: 'totalPaidAmount', header: 'Total Paid Amount' }, // Added missing field
        { field: 'owedAmount', header: 'Owed Amount' }, // Added missing field
        { field: 'serviceCharge', header: 'Service Charge' },
        { field: 'operationalCharge', header: 'Operational Charge' },
        { field: 'issueDate', header: 'Issue Date' },
        { field: 'dueDate', header: 'Due Date' }, // Optional field
    ];
}

  
  
   loadCompanyInvoices(companyId: string): void {
    this.dataService.getCompanyInvoices(companyId).subscribe(
      (response: any) => {
        if (response && response.data) {
           this.invoiceList = response.data;
           this.filteredInvoices = this.invoiceList;

        } else {
           this.invoiceList = response || [];
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
  
  

  //#region Menu
  loadActionMenu(){
    this.actionMenu = [
      {
        label: 'add',
        icon: 'assets/images/icons/add.png',   
        command: () => {
          this.NewInvoiceDialog();
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

  NewInvoiceDialog() {
    this.generateInvoiceVisible=true;
  }
   //#endregion


   //#region  PageActions
   closeJobDialog() {
    this.reloadPage();
  }

     reloadPage() {
       window.location.reload();
  }
   //#endregion

   //#region filters
   onSearch() {
    // Extract fields from targetColumns dynamically
    const fieldsToSearch = this.targetColumns.map(column => column.field);
  
    // Filter the data based on the fields
    this.filteredInvoices = this.globalColumnControlService.filterData(this.invoiceList, this.searchTerm, fieldsToSearch);
  }
  
  
   //#endregion

}