import { Component, OnInit } from '@angular/core';
 import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { DataService } from '../../../services/DataService';
import { Payment } from '../../../models/payments';
import { FunctionsService } from '../../../services/functions.service';
import { AuthService } from '../../../auth/auth.service';
import { TableColumn } from '../../../models/table-columns';
import { GlobalColumnControlService } from '../../../services/ColumnControls';
import { Invoice } from '../../../models/invoices';
import { Customer } from '../../../models/customer';

@Component({
    selector: 'app-payment-management',
    templateUrl: './payments.component.html',
    styleUrl: '../invoices/invoices.component.css',

    providers: [MessageService, ConfirmationService]
})
export class PaymentsComponent implements OnInit {
 
  showDetails(item: Payment) {
    this.selectedPayment = item;
    this.selectedPayment.paymentID= item.paymentID;
    this.selectedPayment.paymentDate= item.paymentDate;
    this.selectedPayment.amountPaid= item.amountPaid;
    this.selectedPayment.currency= item.currency;
    this.selectedPayment.referenceNumber= item.referenceNumber;
    this.selectedPayment.paymentMethod= item.paymentMethod;
     this.displayPaymentDialog = true;
  }
//#region Defaults Objects
 defaultCustomerDetails: Customer  = {
  customerID: '',
  fullName: '',
  email: '',
  phone: '',
  address: '',
 
    cardNumber: '',
    cardType: '',
    paymentMethod: '',
    billingAddress: '',
    expiryDate: '',
    bankName: '',
    bankAccountNumber: '',
    bankAccountHolder: '',
    mobileNetwork: '',
    mobileNumber: '',
  
  companies: []
};

 defaultInvoice: Invoice = {
  invoiceNumber: 0,
  companyID: '',
  customerID: '',
  jobRequestID: '',
  paymentId: null,
  totalAmount: 0,
  totalPaidAmount: 0,
  owedAmount: 0,
  serviceCharge: 0,
  operationalCharge: 0,
  issueDate: new Date().toISOString(),
  dueDate: null,
  status: '',
  customerDetails: this.defaultCustomerDetails,
};

//#endregion
  
    payments: Payment[] = [];
    selectedPayment: any= null;
    displayDialog: boolean = false;
    newPayment: boolean = false;
    
    payment: Payment = {
      paymentID: '',
      invoiceNumber: 0,
      amountPaid: 0,
      paymentDate: new Date(),
      paymentMethod: '',
      referenceNumber: null,
      currency: '',
      invoice: this.defaultInvoice,
    }; // Holds payment details during creation/edit

     invoiceDetails: Invoice|null=null;
    companyId!: string;
    currencies =[
      {label:'Tanzania Shillings (TZs)',value: 'TZS'},
      {label:'US DOLLERS (USD)',value: 'USD'}
    ]
    paymentMethods = [
      { label: 'Credit Card', value: 'Credit Card' },
      { label: 'Bank Transfer', value: 'Bank Transfer' },
      { label: 'Cash', value: 'Cash' },
      { label: 'Mobile Payment', value: 'Mobile Payment' }
    ];
    showPaymentFormDetails: boolean= false;
    filteredPayments: Payment[]=[];
    sourceColumns: TableColumn[] = []; // Available columns
    targetColumns: TableColumn[] = []; // Selected columns
    searchTerm: string = '';
    columnDialogVisible: boolean= false;
displayPaymentDialog: boolean= false;
    constructor(
        private dataServices: DataService,
        public functions: FunctionsService,
        private authServices: AuthService,
         public globalColumnControlService: GlobalColumnControlService,
    ) {}

    ngOnInit(): void {
      this.companyId = this.authServices.getCompanyId() || '';
        this.loadColumns();
        this.loadPayments(this.companyId);
    }

    loadColumns() {
      this.sourceColumns = [
        // Payment Details
        { field: 'paymentID', header: 'Payment ID' },
       
        { field: 'invoice.issueDate', header: 'Issue Date'},
        { field: 'invoice.customerDetails.email', header: 'Customer Email' },
        { field: 'invoice.customerDetails.address', header: 'Customer Address' },
        { field: 'invoice.customerDetails.paymentMethod', header: 'Preferred Payment Method' },
        { field: 'referenceNumber', header: 'Reference Number' },
        { field: 'invoice.totalAmount', header: 'Total Amount' },
        { field: 'invoice.totalPaidAmount', header: 'Total Paid Amount' },
      ];
      this.targetColumns = [ 
        { field: 'invoiceNumber', header: 'Invoice Number' },
        { field: 'paymentDate', header: 'Payment Date'},
        { field: 'amountPaid', header: 'Amount Paid' },
        { field: 'invoice.owedAmount', header: 'Owed Amount'},
        { field: 'paymentMethod', header: 'Payment Method' },
        { field: 'currency', header: 'Currency' },
        // Invoice Details

        // { field: 'invoice.status', header: 'Invoice Status'},

   
      
    
        // Customer Details
        { field: 'invoice.customerDetails.fullName', header: 'Customer Name' },
         { field: 'invoice.customerDetails.phone', header: 'Customer Phone' },
 
      ];
    }
    
getNestedValue(rowData: any, field: string): any {
  return field.split('.').reduce((acc, part) => acc && acc[part], rowData) || null;
}

onSearch() {
  // Extract fields from targetColumns dynamically
  const fieldsToSearch = this.targetColumns.map(column => column.field);

  // Filter the data based on the fields
  this.filteredPayments = this.globalColumnControlService.filterData(this.payments, this.searchTerm, fieldsToSearch);
}
   

    
    loadPayments(companyId: string): void {
      this.dataServices.getCompanyPayments(companyId).subscribe(
        (response: any) => {
          if (response && response.data) {
             this.payments = response.data;
             this.filteredPayments = this.payments;
  
          } else {
             this.payments = response || [];
            this.functions.displayInfo('Received an unexpected response format');
          }
        },
        (error: any) => {
          const errorMessage =
          error.error?.message ||  
          (typeof error.error === 'string' ? error.error : null) ||  
          error.message || // General HTTP error message
          'An unknown error occurred';
        this.functions.displayError(errorMessage);
        }
      );
    }
    onAddPayment() {
        this.newPayment = true;
        this.displayDialog = true;
    }

    onEditPayment(payment: any) {
        this.newPayment = false;
        this.payment = { ...payment };
        this.displayDialog = true;
     }
  
    loadCompanyInvoicesDetails(InvoiceNumber: number): void {
      this.dataServices.getCompanyInvoiceDetails(this.companyId,InvoiceNumber).subscribe(
        (response: any) => {
          if (response && response.data) {
             this.invoiceDetails = response.data;
             this.payment.amountPaid = this.invoiceDetails?.owedAmount || 0;
             this.showPaymentFormDetails=true;
          } else {
             this.invoiceDetails = response || [];
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
    
    savePayment() {
      if (this.payment) {
          this.dataServices.addPayment(this.payment).subscribe({
              next: (response: any) => {
                  if (response && response.success) {
                      this.loadPayments(this.companyId);
                      this.displayDialog = false;
                      this.functions.displaySuccess('Payment added successfully');
                  } else {
                      const errorMessage = response?.message || 'Failed to add payment';
                      this.functions.displayError(errorMessage);
                  }
              },
              error: (error) => {
                  const errorMessage =
                      error.error?.message ||
                      (typeof error.error === 'string' ? error.error : null) ||
                      error.message ||
                      'An unknown error occurred';
                  this.functions.displayError(errorMessage);
              },
          });
      }
      // this.reloadPage(); 

  }
  

  deletePayment(paymentID: string): void {
    this.dataServices.deletePayment(paymentID).subscribe(
      (response: any) => {
        if (response && response.success) {
          // this.payments = this.payments.filter((p) => p.paymentID !== paymentID);
          this.functions.displaySuccess('Payment deleted successfully');
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
  

  closePaymentDialog() {
    this.selectedPayment = null;
    this.displayPaymentDialog = false;
    this.reloadPage();
  }

   reloadPage() {
    window.location.reload();
  }
  }
