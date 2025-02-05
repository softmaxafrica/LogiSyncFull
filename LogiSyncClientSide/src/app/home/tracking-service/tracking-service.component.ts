import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tracking-service',
  templateUrl: './tracking-service.component.html',
  styleUrls: ['./tracking-service.component.css']
})
export class TrackingServiceComponent implements OnInit {
  // Define truckLocations with a more specific type
  truckLocations: { [key: string]: { latitude: number, longitude: number } } = {}; 
  truckId: string | null = null;
  showInfoPanel: boolean = true;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    // Subscribe to route parameters and handle truckId
    this.route.params.subscribe(params => {
      this.truckId = params['truckId'] || null;

      if (this.truckId) {
        console.log('Tracking Truck ID:', this.truckId);
        // Track a single truck if truckId is provided
        this.trackSingleTruck(this.truckId);
      } else {
        console.log('No Truck ID provided. Displaying all trucks.');
        // Track multiple trucks if no truckId is provided
        this.trackAllTrucks();
      }
    });
  }

  // Track a specific truck
  trackSingleTruck(truckId: string): void {
    console.log(`🔍 Tracking truck: ${truckId}`);
    // Replace with actual logic to track the specific truck
    // For example, you could fetch truck data based on truckId and update truckLocations
    this.truckLocations[truckId] = { latitude: 37.7749, longitude: -122.4194 };  // Sample coordinates
  }

  // Track all trucks
  trackAllTrucks(): void {
    console.log(`📍 Tracking all trucks in real-time.`);
    // Replace with actual logic to track all trucks, e.g., fetch real-time data for all trucks
    this.truckLocations = {
      'Truck 1': { latitude: 37.7749, longitude: -122.4194 },
      'Truck 2': { latitude: 37.8044, longitude: -122.2711 },
      'Truck 3': { latitude: 37.6879, longitude: -122.4702 }
    };
  }

  // Toggle Info Panel visibility
  toggleInfoPanel(): void {
    this.showInfoPanel = !this.showInfoPanel;
  }
}
