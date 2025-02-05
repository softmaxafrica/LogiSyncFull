import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tracking-service',
  templateUrl: './tracking-service.component.html',
  styleUrls: ['./tracking-service.component.css']
})
export class TrackingServiceComponent implements OnInit {
truckLocations: ReadonlyMap<unknown, unknown> | undefined;
toggleInfoPanel() {
throw new Error('Method not implemented.');
}
  truckId: string | null = null;
showInfoPanel: any;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.truckId = params['truckId'] || null; // Assign null if truckId is not provided

      if (this.truckId) {
        console.log('Tracking Truck ID:', this.truckId);
        // Start tracking single truck
        this.trackSingleTruck(this.truckId);
      } else {
        console.log('No Truck ID provided. Displaying all trucks.');
        // Start tracking multiple trucks
        this.trackAllTrucks();
      }
    });
  }

  trackSingleTruck(truckId: string) {
    console.log(`🔍 Tracking truck: ${truckId}`);
    // Implement single truck tracking logic here
  }

  trackAllTrucks() {
    console.log(`📍 Tracking all trucks in real-time.`);
    // Implement multiple truck tracking logic here
  }
}
