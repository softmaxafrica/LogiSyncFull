import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { LandingComponent } from './home/landing/landing.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { InvoicesComponent } from './home/billing/invoices/invoices.component';
import { PaymentsComponent } from './home/billing/payments/payments.component';
import { TrucksComponent } from './home/trucks/trucks.component';
import { JobsComponent } from './home/jobs/jobs.component';
import { AuthService } from './auth/auth.service';
import { DriversComponent } from './home/drivers/drivers.component';
import { DriverRegistrationComponent } from './home/drivers/driver-registration/driver-registration.component';
import { DriverListingComponent } from './home/drivers/driver-listing/listing.component';
import { DriverVettingComponent } from './home/drivers/driver-vetting/driver-vetting.component';
import { JobDetailsComponent } from './home/jobs/job-details/job-details.component';
import { CustomersComponent } from './home/customers/customers.component';
    
const routes: Routes = [
  { path: '', redirectTo: 'home/landing', pathMatch: 'full' },
  { path: 'home/landing', component: LandingComponent },
  { path: 'home/dashboard', component: DashboardComponent, canActivate: [AuthService] },
  { path: 'home/billing/invoices', component: InvoicesComponent, canActivate: [AuthService] },
  { path: 'home/billing/payments', component: PaymentsComponent, canActivate: [AuthService] },
 
  { 
    path: 'home/drivers/listing',component: DriverListingComponent, canActivate: [AuthService]},
  { path: 'home/drivers/registration',component: DriverRegistrationComponent,canActivate: [AuthService]},
  { path: 'home/drivers/vetting', component: DriverVettingComponent, canActivate: [AuthService]},
  
  { path: 'home/customers', component: CustomersComponent,canActivate: [AuthService] },
  
  { path: 'home/trucks', component: TrucksComponent, canActivate: [AuthService] },
  
  { path: 'home/jobs', component: JobsComponent, canActivate: [AuthService] },
  { path: 'job-details/:jobRequestID', component: JobDetailsComponent , canActivate: [AuthService]},
  
  { path: 'account/login', component: LoginComponent },
  { path: 'account/registration', component: RegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
