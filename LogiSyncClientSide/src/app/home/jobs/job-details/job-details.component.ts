import { Component, OnInit } from '@angular/core';
import { JobRequest } from '../../../models/jobRequest';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from '../../../services/DataService';
import { RequestWithPrice } from '../../../models/jobRequestPayload';

@Component({
  selector: 'app-job-details',
  templateUrl: './job-details.component.html',
  styleUrl: './job-details.component.css'
})
export class JobDetailsComponent {
finalizeAgreement() {
throw new Error('Method not implemented.');
}
 
saveChanges() {
throw new Error('Method not implemented.');
}

  job: RequestWithPrice |null=null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dataservices: DataService){

  }
  ngOnInit(): void{
    //get the job from state 

    const navigation= this.router.getCurrentNavigation();
    this.job= navigation?.extras.state?.['job'];
//If state fails try from server
    if(!this.job){
      const jobId= this.route.snapshot.paramMap.get('jobRequestID');
      this.dataservices.fetchRequestById(jobId);
    }
  }
}
