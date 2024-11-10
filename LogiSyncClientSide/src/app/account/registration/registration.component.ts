import { Component } from '@angular/core';
import { CompanyPayload } from '../../models/CompanyPayload';
import { DataService } from '../../services/DataService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css','../../app.component.css']
})
export class RegistrationComponent {
  companyPayload: CompanyPayload = new CompanyPayload();

  constructor(private dataService: DataService, private router: Router) {}

  onRegister(): void {
    if (this.isFormValid()) {
      this.dataService.post<CompanyPayload>('RegisterCompany', this.companyPayload)
        .subscribe({
          next: (response: any) => {
            console.log('Company registered successfully', response);
            this.router.navigate(['/account/login']);  // Navigate to the appropriate page after successful registration
          },
          error: (err: any) => {
            console.error('Registration failed', err);
          }
        });
    } else {
      console.warn('Please fill out all required fields');
    }
  }

  isFormValid(): boolean {
    return this.companyPayload.companyName != null && 
           this.companyPayload.adminEmail != null && 
           this.companyPayload.adminFullName != null && 
           this.companyPayload.adminContact != null && 
           this.companyPayload.companyAddress != null;
  }
}
