import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
 
import { LoginComponent } from './useraccount/login/login.component';
import { CompanyregistrationComponent } from './useraccount/companyregistration/companyregistration.component';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';  // Import FormsModule
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DividerModule } from 'primeng/divider';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { SplitterModule } from 'primeng/splitter';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CompanyregistrationComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    //My IMports

    CardModule,
    DividerModule,
    SplitterModule,
    FloatLabelModule,
    FormsModule,
    ButtonModule

  ],
  providers: [],
  bootstrap: [
    AppComponent,LoginComponent
  ]
})
export class AppModule { }
