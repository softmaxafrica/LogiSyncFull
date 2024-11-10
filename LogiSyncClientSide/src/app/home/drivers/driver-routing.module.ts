import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
 import { DriverRegistrationComponent } from './driver-registration/driver-registration.component';
import { DriverVettingComponent } from './driver-vetting/driver-vetting.component';
import { AuthService } from '../../auth/auth.service';
import { DriverListingComponent } from './driver-listing/listing.component';
import { AssignmentsComponent } from './assignments/assignments.component';
  
const routes: Routes = [
  { path: 'listing', component: DriverListingComponent },
  { path: 'registration', component: DriverRegistrationComponent, canActivate: [AuthService] },
  { path: 'security/vetting', component: DriverVettingComponent, canActivate: [AuthService] },
   { path: 'assignments', component: AssignmentsComponent },  // Example route for Assignments
   
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DriverRoutingModule { }
