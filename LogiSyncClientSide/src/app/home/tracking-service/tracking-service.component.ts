import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

declare var google: any;

@Component({
  selector: 'app-tracking-service',
  templateUrl: './tracking-service.component.html',
  styleUrls: ['./tracking-service.component.css']
})
export class TrackingServiceComponent implements OnInit, AfterViewInit {
  truckLocations: { [key: string]: { latitude: number; longitude: number } } = {};
  truckId: string | null = null;
  showInfoPanel: boolean = true;
  map: any;
  markers: any[] = []; // Store markers for easy updates

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.truckId = params['truckId'] || null;

      if (this.truckId) {
        console.log(`🔍 Tracking single truck: ${this.truckId}`);
        this.trackSingleTruck(this.truckId);
      } else {
        console.log('📍 Tracking all trucks in real-time.');
        this.trackAllTrucks();
      }
    });
  }

  ngAfterViewInit(): void {
    this.loadMap();
  }

  loadMap(): void {
    if (typeof google !== 'undefined' && google.maps) {
      const mapElement = document.getElementById("map") as HTMLElement;
      this.map = new google.maps.Map(mapElement, {
        center: { lat: 37.7749, lng: -122.4194 },
        zoom: 8,
      });
  
      this.updateMarkers(); // Add initial markers
    } else {
      console.error('Google Maps API failed to load.');
    }
  }
  

  updateMarkers(): void {
    if (!this.map) return;

    // Clear old markers
    this.markers.forEach(marker => marker.setMap(null));
    this.markers = [];

    // Add new markers using AdvancedMarkerElement
    Object.keys(this.truckLocations).forEach(truckId => {
      const location = this.truckLocations[truckId];

      const marker = new google.maps.Marker({
        position: { lat: location.latitude, lng: location.longitude },
        map: this.map,
        title: truckId
      });
      

      this.markers.push(marker);
    });
  }

  trackSingleTruck(truckId: string): void {
    this.truckLocations = {
      [truckId]: { latitude: 37.7749, longitude: -122.4194 } // Example
    };
    this.updateMarkers();
  }

  trackAllTrucks(): void {
    this.truckLocations = {
      'Truck 1': { latitude: 37.7749, longitude: -122.4194 },
      'Truck 2': { latitude: 37.8044, longitude: -122.2711 },
      'Truck 3': { latitude: 37.6879, longitude: -122.4702 }
    };
    this.updateMarkers();
  }

  toggleInfoPanel(): void {
    this.showInfoPanel = !this.showInfoPanel;
  }
}
