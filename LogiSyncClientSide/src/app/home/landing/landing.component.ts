import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrl:   '../../app.component.css', 

 })
export class LandingComponent implements OnInit {
  showWelcomeContent = true;

  constructor() {}

  ngOnInit() {
 
  }

  hideWelcomeContent() {
    this.showWelcomeContent = false;
  }
}
