import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from 'primeng/api';
import { DataService } from '../../services/DataService';
import { TrucksPayload } from '../../models/TrucksPayload';
import { FunctionsService } from '../../services/functions.service';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { Sidebar } from 'primeng/sidebar';
import { TruckType } from '../../models/TruckTypes';
import { Truck } from '../../models/Truck';
import { DriverPayload } from '../../models/drivers';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { CarouselResponsiveOptions } from 'primeng/carousel';

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['../../app.component.css','./trucks.component.css']
})
export class TrucksComponent implements OnInit {
  responsiveOptions: any[] | undefined;

onSubmit() {
   console.log(this.truckPayload);
}

  currentDriverName: string = ''; // Updated to default to empty string
  isTruckTypeSelected: boolean = false;
  filteredTruckTypes: TruckType[] = [];
  cabinType: any[] = []; // Initialize your dropdown options
  category: any[] = [];
  fuelType: any[] = [];
  drive: any[] = [];
  selectedDriver: DriverPayload | null = null;
  selectedTruckType: TruckType | null = null;
  companyId: string = ''; // Default to empty string

  truckPayload: TrucksPayload = {
    truckID: '',   
    truckNumber: '',
    model: '',
    truckTypeID: '',
    companyID: '',
    isActive: true,
    isTruckAvilableForBooking: true,
    driverID: '',
    chasisNo: '',
    brand: '',
    engineCapacity: '',
    fuelType: '',
    cabinType: '',
    category: '',
    drive: ''
  };
   
  trucks: Truck[] = [];
  truckTypes: TruckType[] = [];
  companyDrivers: DriverPayload[] = [];
  
  newTruckVisibility: boolean = false;
  truckForm: FormGroup;
  truckmenu: MenuItem[] = [];
  sidebarVisible: boolean = false;
  editTruckDialog: boolean= false;
 

  constructor(
    private dataService: DataService,
    public functions: FunctionsService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.truckForm = this.fb.group({
      truckNumber: ['', Validators.required],
      model: ['', Validators.required],
      chasisNo: ['', Validators.nullValidator],  // Optional field
      brand: ['', Validators.nullValidator],
      engineCapacity: ['', Validators.nullValidator],
      fuelType: ['', Validators.nullValidator],
      cabinType: ['', Validators.nullValidator],
      category: ['', Validators.nullValidator],
      drive: ['', Validators.nullValidator],
    });
  }

  ngOnInit(): void {
    this.companyId = this.authService.getCompanyId() || '';
    this.truckPayload.companyID = this.companyId;
this.updateTruckSubMenu()
    this.getTruckTypes();
    this.loadDropdownOptions();
    this.GetTruckByCompanyId();
    this.getCompanyDrivers();

    this.responsiveOptions = [
      {
          breakpoint: '1199px',
          numVisible: 1,
          numScroll: 1
      },
      {
          breakpoint: '991px',
          numVisible: 2,
          numScroll: 1
      },
      {
          breakpoint: '767px',
          numVisible: 1,
          numScroll: 1
      }
  ];
  }

  getCompanyDrivers() {
    this.dataService.getCompanyDrivers(this.companyId).subscribe(
      (response) => {
        this.companyDrivers = response.data;
      },
      (error) => {
        const errorMessage = error.error || error.message || 'An unknown error occurred';
        this.functions.displayError(errorMessage);
      }
    );
  }

  loadDropdownOptions(): void {
    // Example data, replace with actual data retrieval logic
    this.cabinType = [
      { label: 'SINGLE CABIN', value: 'SINGLE CABIN' },
      { label: 'DOUBLE CABIN', value: 'DOUBLE CABIN' }
    ];
    this.category = [
      { label: 'HORSE TRUCK', value: 'HORSE' },
      { label: 'TRAILER TRUCK', value: 'TRAILER' }
    ];
    this.fuelType = [
      { label: 'DIESEL TYPE', value: 'DIESEL' },
      { label: 'PETROL TYPE', value: 'PETROL' },
      { label: 'ELECTRICAL TYPE', value: 'ELECTRICAL' }

    ];
    this.drive = [
      { label: 'LEFT HANDED DRIVE', value: 'LEFT HANDED DRIVE' },
      { label: 'RIGHT HANDED DRIVE', value: 'RIGHT HANDED DRIVE' }
    ];
  }

  public getTruckTypes(): void {
    this.dataService.GetTruckTypes().subscribe(
      (response) => {
        this.truckTypes = response.data;
      },
      (error) => {
        const errorMessage = error.error || error.message || 'An unknown error occurred';
        this.functions.displayError(errorMessage);
      }
    );
  }

  updateTruckDetails(updatedTruck: TrucksPayload, isActive: boolean) {
    updatedTruck.isActive = isActive;
    updatedTruck.isTruckAvilableForBooking = updatedTruck.isActive ? updatedTruck.isTruckAvilableForBooking : false;
    this.truckPayload = { ...updatedTruck };
    this.dataService.updateTruckDetails(updatedTruck).subscribe({
      next: (response: any) => {
        this.functions.displayInfo('Truck number: ' + updatedTruck.truckNumber + ' is now ' + (isActive ? 'ACTIVE' : 'INACTIVE'));
      },
      error: (err) => {
        this.functions.displayError('Failed to update truck status: ' + err.message);
      }
    });
    this.ngOnInit();
    this.editTruckDialog=false;
  }

  updateFullTruckDetails(updatedTruck: TrucksPayload, newDriverId?: string) {
    updatedTruck.isTruckAvilableForBooking = updatedTruck.isActive ? updatedTruck.isTruckAvilableForBooking : false;
    this.truckPayload = { ...updatedTruck };
    this.truckPayload.driverID = newDriverId ? newDriverId : this.truckPayload.driverID || ''; // Provide default value

    this.dataService.updateTruckDetails(updatedTruck).subscribe({
      next: (response: any) => {
        this.functions.displayUpdateSuccess();
      },
      error: (err) => {
        this.functions.displayError('Failed to update truck status: ' + err.message);
      }
    });
    this.editTruckDialog=false;
    this.ngOnInit();
  }

  GetTruckByCompanyId(): void {
    this.dataService.GetTruckByCompanyId(this.companyId).subscribe(
      (response) => {
        if (response && response.data) {
          this.trucks = response.data;
          this.trucks.forEach(truck => {
            this.isTruckTypeSelected = truck.isActive; // Adjusted to use correct boolean
          });
        }
      },
      (error) => {
        const errorMessage = error.error || error.message || 'An unknown error occurred';
        this.functions.displayError(errorMessage);
      }
    );
  }

  onAddTruck() {
    if (this.isFormValid()) {
      this.truckPayload.truckTypeID = this.selectedTruckType?.truckTypeID || '';
      this.truckPayload.isActive = true;
      this.truckPayload.isTruckAvilableForBooking = true;

      // // Map the form values to truckPayload
      // this.truckPayload.chasisNo = this.truckForm.value.chasisNo  ; 
      // this.truckPayload.brand = this.truckForm.value.brand  ; 
      // this.truckPayload.engineCapacity = this.truckForm.value.engineCapacity  ; 
      // this.truckPayload.fuelType = this.truckForm.value.fuelType  ; 
      // this.truckPayload.cabinType = this.truckForm.value.cabinType  ; 
      // this.truckPayload.category = this.truckForm.value.category; 
      // this.truckPayload.drive = this.truckForm.value.drive; 
console.log(this.truckPayload);

      this.dataService.postTruck<TrucksPayload>('AddTruck', this.truckPayload)
        .subscribe({
          next: (response: any) => {
            if (response.success) {
              this.newTruckVisibility = false;
              this.ngOnInit();
              this.functions.displayInsertSuccess();
            } else {
              this.newTruckVisibility = false;
              this.functions.displayError(response.message);
            }
          },
          error: (err: any) => {
            this.newTruckVisibility = false;
            const errorMessage = err.error || 'unknown_error';
            this.functions.displayError('data_insertion_failed \n' + errorMessage);
          }
        });
    } else {
      this.functions.displayInfo('please_fill_all_the_required_fields');
    }
  }

  isFormValid(): boolean {
    return this.truckPayload.truckNumber.trim() !== '' &&
           this.truckPayload.model.trim() !== '' &&
           this.truckPayload.truckTypeID.trim() !== '';
  }

  onTruckTypeChange(event: any): void {
    this.selectedTruckType = event.value;
    this.truckPayload.truckTypeID = this.selectedTruckType?.truckTypeID || ''; // Handle undefined

    this.isTruckTypeSelected = true;
    if (this.selectedTruckType) {
      this.filteredTruckTypes = this.truckTypes.filter(truckType => truckType.truckTypeID === this.selectedTruckType!.truckTypeID);
    } else {
      this.filteredTruckTypes = [];  
    }
  }

  onCabinTypeChange(event: any) {
    console.log('Cabin Type Event:', event); // Log event details
    this.truckPayload.cabinType = event.value; // Assign selected value to truckPayload
    console.log('Selected Cabin Type:', this.truckPayload.cabinType);
}


  onCategoryChange(event: any) {
    this.truckPayload.category = event.value; // Assign selected value to truckPayload
    console.log('Selected Category:', this.truckPayload.category);
  }

  onFuelTypeChange(event: any) {
    this.truckPayload.fuelType = event.value; // Assign selected value to truckPayload
    console.log('Selected Fuel Type:', this.truckPayload.fuelType);
  }

  onDriveChange(event: any) {
    this.truckPayload.drive = event.value;  
    console.log('Selected Drive Type:', this.truckPayload.drive);
  }

  onEditTruck(truck: TrucksPayload) {
     
    this.truckPayload = { ...truck };
    const driver = this.companyDrivers.find(driver => driver.driverID === this.truckPayload.driverID);
    
    this.currentDriverName = driver ? driver.fullName : 'N/A';
    
    // Populate the form with existing truck details
    this.truckForm.setValue({
      truckNumber: this.truckPayload.truckNumber,
      model: this.truckPayload.model,
      chasisNo: this.truckPayload.chasisNo  ,
      brand: this.truckPayload.brand  ,
      engineCapacity: this.truckPayload.engineCapacity  ,
      fuelType: this.truckPayload.fuelType  ,
      cabinType: this.truckPayload.cabinType  ,
      category: this.truckPayload.category  ,
      drive: this.truckPayload.drive  ,
    });
    
    this.editTruckDialog = true;
  }
  onChangeTruckDriver(event: DropdownChangeEvent) {
    this.selectedDriver = event.value; 
     this.truckPayload.driverID = this.selectedDriver?.driverID ?? ''; // Fallback to empty string if undefined

  }
  onDeleteTruck(truckID: string) {
    // this.dataService.deleteTruck(truckID).subscribe({
    //   next: () => {
    //     this.functions.displayDeleteSuccess();
    //     this.GetTruckByCompanyId();
    //   },
    //   error: (err) => {
    //     const errorMessage = err.error || 'unknown_error';
    //     this.functions.displayError('deletion_failed \n' + errorMessage);
    //   }
    // });
  }
  updateTruckSubMenu() {
    this.truckmenu = [
      {
        label: 'Add',
        icon: 'assets/images/icons/add.png',   
        command: () => {
          this.newTruckVisibility = true;
        }
      },
      
      {
        label: 'Locate',
        icon: 'assets/images/icons/tracking-delivery.png',   
        command: () => {}
      },
 
            
     
    ];
  }


  
}
