import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
  
 
  import { MenuItem } from 'primeng/api';
import { ImportsModule } from '../../imports';
import { DataService } from '../../services/DataService';
import { DriverPayload } from '../../models/drivers';
import { TableColumn } from '../../models/table-columns';
import { AuthService } from '../../auth/auth.service';

@Component({
    selector: 'table-paginator-programmatic-demo',
    templateUrl: './drivers.component.html',
    standalone: true,
    imports: [ImportsModule],
    providers: [DataService]
})
export class DriversComponent implements OnInit {
    //Testing Driver List
         drivers: DriverPayload[] = [];
         driverOptions: MenuItem[]|undefined;
         companyId!:string;
        first = 0;
        rows = 10;
    
        sourceColumns: TableColumn[] = []; // Available columns
        targetColumns: TableColumn[] = []; // Selected columns
    
    
        constructor(
            private dataService: DataService,
            private authService: AuthService
        ) {}
    
        ngOnInit(): void {
            
            this.companyId = this.authService.getCompanyId() || '';
            this.dataService.getCompanyDrivers(this.companyId).subscribe((response) => {
                this.drivers = response.data || [];
             });
            
            // Define your available columns
            this.sourceColumns = [
                { field: 'licenseNumber', header: 'License Number' },
                 { field: 'companyID', header: 'Company ID' }
            ];
    
            // Initialize selected columns with default values
            this.targetColumns = [
                { field: 'fullName', header: 'Full Name' },
              { field: 'email', header: 'Email' },
              { field: 'phone', header: 'Phone' },
                { field: 'status', header: 'Status' }
            ];
        }
    
        next(): void {
            this.first = this.first + this.rows;
        }
    
        prev(): void {
            this.first = this.first - this.rows;
        }
    
        reset(): void {
            this.first = 0;
        }
    
        pageChange(event: any): void {
            this.first = event.first;
            this.rows = event.rows;
        }
    
        isLastPage(): boolean {
            return this.drivers.length ? this.first === this.drivers.length - this.rows : true;
        }
    
        isFirstPage(): boolean {
            return this.first === 0;
        }
    
 
 
}
 
