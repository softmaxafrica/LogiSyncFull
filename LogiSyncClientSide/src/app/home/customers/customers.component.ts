import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { GlobalColumnControlService } from '../../services/ColumnControls';
import { DataService } from '../../services/DataService';
import { FunctionsService } from '../../services/functions.service';
import { MenuItem } from 'primeng/api';
import { AppConstants } from '../../services/shared/Constants';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
 })
export class CustomersComponent implements OnInit {
    
  

onRegister() {
  if (!this.customerRegData.companies.includes(this.companyId)) {
    this.customerRegData.companies.push(this.companyId);

  }
this.dataService.postCustomer<Customer>('RegisterCustomer',this.customerRegData)
.subscribe({
  next:(response:any) =>{
    if(response.success){
      this.showCustomerRegistration=false;
      this.functions.displayInsertSuccess();
      this.getCompanyCustomers();
    }
    else{
      this.functions.displayInfo(response.message);
    }
  
  },
error: (err: any) =>{
  this.showCustomerRegistration= false;
  const errorMessage= err.error || 'unkown error';
  this.functions.displayError('customer registration failed : '+ errorMessage);
}  
});
}


editCustomer(_t24: any) {
throw new Error('Method not implemented.');
}

confirmDelete(_t24: any) {
throw new Error('Method not implemented.');
}
showCustomerRegistration: boolean=false;
columnDialogVisible: any;
Customers: Customer[] = [];
editDialogVisible: boolean=false; 
CustomerForm!: FormGroup;
deleteDialogVisible: boolean = false;
companyId!: string;
actionMenu: MenuItem[] | null | undefined;
public baseUrl = AppConstants.API_SERVER_URL;

paymentMethods: any[] = [
  { label: 'Credit Card', value: 'Credit Card' },
  { label: 'Bank Transfer', value: 'Bank Transfer' },
  { label: 'Tigo Pesa', value: 'Tigo Pesa' },
  { label: 'Mpesa', value: 'Mpesa' },
  { label: 'TTCL', value: 'TTCL' },
  { label: 'Cash', value: 'Cash' }
];

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
this.companyId= this.authService.getCompanyId() || '';
this.getCompanyCustomers();
this.loadActionMenu();

    // Initialize the form with validations
    this.CustomerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: [''],
      paymentInfo: [''],
      companyID: ['']      
    });
    
  }
  initializeColumns() {
    this.globalColumnControlService.setSourceColumns([
       { field: 'CustomerID', header: 'Customer ID' },
       { field: 'cardNumber', header: 'Card Number' },         
       { field: 'cardType', header: 'Card Type' },             
       { field: 'billingAddress', header: 'Billing Address' },  
       { field: 'expiryDate', header: 'Expiry Date' },        
       { field: 'bankName', header: 'Bank Name' },            
       { field: 'bankAccountNumber', header: 'Bank Account Number' },  
       { field: 'bankAccountHolder', header: 'Bank Account Holder' },  
       { field: 'mobileNetwork', header: 'Mobile Network' },  
       { field: 'mobileNumber', header: 'Payment Number' },    
    ]);

    this.globalColumnControlService.setTargetColumns([
       { field: 'fullName', header: 'Full Name' },
       { field: 'email', header: 'Email' },
       { field: 'address', header: 'Address' },
       { field: 'paymentMethod', header: 'Payment Method' },  
       { field: 'phone', header: 'Phone' }
    ]);
}

  
  loadActionMenu() {
    this.actionMenu = [
      {
        label: 'All',
        icon: 'assets/images/icons/test-drive.png',
        command: () => {
          this.showCustomerRegistration=false;
        }
      },
      {
        label: 'Add',
        icon: 'assets/images/icons/add.png',
        command: () => {
          this.showCustomerRegistration=true;
         }
      },
       

    ];
  }
  //#region Get Customers
  getCompanyCustomers(): void {
    this.dataService.GetCustomersByCompany(this.companyId).subscribe(
      (response) => {
        this.Customers = response.data || [];
      },
      (error) => {
        const errorMessage = error.error || error.message || 'An unknown error occurred';
        this.functions.displayError(errorMessage); // Use FunctionsService for error notification
      }
    );
  }
//#region Customer data model instance
customerRegData: Customer = {
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

// Populate form with Customer data
populateForm(customer: Customer): void {
  this.CustomerForm.setValue({
      fullName: customer.fullName,
      email: customer.email,
      phone: customer.phone,
      customerID: customer.customerID,
      address: customer.address || '',
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
   });
}
//#endregion

}
