 import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Correct import
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { DashboardComponent } from './home/dashboard/dashboard.component';
import { LandingComponent } from './home/landing/landing.component';
import { RegistrationComponent } from './account/registration/registration.component';
import { LoginComponent } from './account/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // Import FormsModule
 import { UserService } from './services/UserService';
 import { AuthService } from './auth/auth.service';
 import { Sidebar } from 'primeng/sidebar';


import { DividerModule } from 'primeng/divider';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { SplitterModule } from 'primeng/splitter';
import { ToolbarModule } from 'primeng/toolbar';
import { AvatarModule } from 'primeng/avatar';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { SidebarModule } from 'primeng/sidebar';
import { MenuModule } from 'primeng/menu';
import { MegaMenuModule } from 'primeng/megamenu';
import { MenubarModule } from 'primeng/menubar';
import { TrucksComponent } from './home/trucks/trucks.component';
 import { JobsComponent } from './home/jobs/jobs.component';
import { InvoicesComponent } from './home/billing/invoices/invoices.component';
import { PaymentsComponent } from './home/billing/payments/payments.component';
import { PriceAgreementComponent } from './home/billing/price-agreement/price-agreement.component';
 
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { PickListModule } from 'primeng/picklist';


import { MessagesModule } from 'primeng/messages';
import { DragDropModule } from 'primeng/dragdrop';
import { HttpClient, provideHttpClient } from '@angular/common/http';
 import { MessageService, SharedModule } from 'primeng/api';
import { DriverRegistrationComponent } from './home/drivers/driver-registration/driver-registration.component';
import { DriverListingComponent } from './home/drivers/driver-listing/listing.component';
import { FieldsetModule } from 'primeng/fieldset';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { AccordionModule } from 'primeng/accordion';
import { AnimateModule } from 'primeng/animate';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AutoFocusModule } from 'primeng/autofocus';
import { BadgeModule } from 'primeng/badge';
import { BlockUIModule } from 'primeng/blockui';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { CalendarModule } from 'primeng/calendar';
import { CarouselModule } from 'primeng/carousel';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { CheckboxModule } from 'primeng/checkbox';
import { ChipModule } from 'primeng/chip';
import { ChipsModule } from 'primeng/chips';
import { ColorPickerModule } from 'primeng/colorpicker';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DataViewModule } from 'primeng/dataview';
import { DeferModule } from 'primeng/defer';
import { DialogModule } from 'primeng/dialog';
import { DockModule } from 'primeng/dock';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { FocusTrapModule } from 'primeng/focustrap';
import { GalleriaModule } from 'primeng/galleria';
import { IconFieldModule } from 'primeng/iconfield';
import { ImageModule } from 'primeng/image';
import { InplaceModule } from 'primeng/inplace';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputIconModule } from 'primeng/inputicon';
import { InputMaskModule } from 'primeng/inputmask';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputOtpModule } from 'primeng/inputotp';
import { InputSwitchModule } from 'primeng/inputswitch';
import { KnobModule } from 'primeng/knob';
import { ListboxModule } from 'primeng/listbox';
import { MessageModule } from 'primeng/message';
import { MeterGroupModule } from 'primeng/metergroup';
import { MultiSelectModule } from 'primeng/multiselect';
import { OrderListModule } from 'primeng/orderlist';
import { OrganizationChartModule } from 'primeng/organizationchart';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { PaginatorModule } from 'primeng/paginator';
import { PanelModule } from 'primeng/panel';
import { PanelMenuModule } from 'primeng/panelmenu';
import { ProgressBarModule } from 'primeng/progressbar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { ScrollerModule } from 'primeng/scroller';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { ScrollTopModule } from 'primeng/scrolltop';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SkeletonModule } from 'primeng/skeleton';
import { SlideMenuModule } from 'primeng/slidemenu';
import { SliderModule } from 'primeng/slider';
import { SpeedDialModule } from 'primeng/speeddial';
import { SpinnerModule } from 'primeng/spinner';
import { SplitButtonModule } from 'primeng/splitbutton';
import { StepperModule } from 'primeng/stepper';
import { StepsModule } from 'primeng/steps';
import { StyleClassModule } from 'primeng/styleclass';
import { TabMenuModule } from 'primeng/tabmenu';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { TimelineModule } from 'primeng/timeline';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { TooltipModule } from 'primeng/tooltip';
import { TreeModule } from 'primeng/tree';
import { TreeSelectModule } from 'primeng/treeselect';
import { TreeTableModule } from 'primeng/treetable';
import { TriStateCheckboxModule } from 'primeng/tristatecheckbox';
import { VirtualScrollerModule } from 'primeng/virtualscroller';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
 import { LanguageLoader } from './services/shared/languageLoader';
import { TranslateModule, TranslateLoader, TranslateService } from '@ngx-translate/core';
import { DriverVettingComponent } from './home/drivers/driver-vetting/driver-vetting.component';
import { JobDetailsComponent } from './home/jobs/job-details/job-details.component';
import { CustomersComponent } from './home/customers/customers.component';
   export function HttpLoaderFactory(httpClient: HttpClient) {
  return new TranslateHttpLoader(httpClient);
}

@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    JobDetailsComponent,
     LoginComponent,
    LandingComponent,
    RegistrationComponent,
    DriverRegistrationComponent,
    DriverListingComponent,
    DriverVettingComponent,
    DashboardComponent,
    TrucksComponent,
     JobsComponent,
    InvoicesComponent,
    PaymentsComponent,
    PriceAgreementComponent,
    TrucksComponent
    ],
  imports: [
    
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: LanguageLoader,
        deps: [HttpClient], // HttpClient is required
      }
    }),
    ReactiveFormsModule ,
    TieredMenuModule,
    CarouselModule,
    ReactiveFormsModule,
    DropdownModule ,
    DialogModule,
    FieldsetModule,
    BrowserAnimationsModule,
    BrowserModule,  
    AppRoutingModule,
    DividerModule,
    FloatLabelModule,
    ButtonModule,
    CardModule,
    SplitterModule,
    FormsModule,
    ToolbarModule,
    AvatarModule,
    PasswordModule,
    InputSwitchModule,
    InputTextModule,
    InputTextareaModule,
    SidebarModule,
    MenuModule,
    MegaMenuModule,
    MenubarModule,
    ToastModule,
    ChipModule,
    MessagesModule,
    TableModule,
    PickListModule,
    DragDropModule,
    SharedModule,
    TagModule,
    DockModule,
    FileUploadModule,
    CalendarModule,
    CheckboxModule,
    RadioButtonModule,
    InputNumberModule
    
    
  ],
  exports: [
    ReactiveFormsModule ,
    AvatarModule,
    AvatarGroupModule,
     FormsModule,
     AccordionModule,
    AutoCompleteModule,
    BadgeModule,
    BreadcrumbModule,
    BlockUIModule,
    ButtonModule,
    CalendarModule,
    CarouselModule,
    CascadeSelectModule,
    CheckboxModule,
    ChipsModule,
    ChipModule,
    ColorPickerModule,
    ConfirmDialogModule,
    ConfirmPopupModule,
    ContextMenuModule,
    VirtualScrollerModule,
    DataViewModule,
    DialogModule,
    DividerModule,
    DeferModule,
    DockModule,
    DragDropModule,
    DropdownModule,
    DynamicDialogModule,
    EditorModule,
    FieldsetModule,
    FileUploadModule,
    FocusTrapModule,
    GalleriaModule,
     InplaceModule,
    InputMaskModule,
    InputSwitchModule,
    InputTextModule,
    InputTextareaModule,
    InputNumberModule,
    InputGroupModule,
    InputGroupAddonModule,
    InputOtpModule,
    ImageModule,
    KnobModule,
    ListboxModule,
    MegaMenuModule,
    MenuModule,
    MenubarModule,
    MessageModule,
    MessagesModule,
    MultiSelectModule,
    MeterGroupModule,
    OrganizationChartModule,
    OrderListModule,
    OverlayPanelModule,
    PaginatorModule,
    PanelModule,
    PanelMenuModule,
    PasswordModule,
    PickListModule,
    ProgressSpinnerModule,
    ProgressBarModule,
    RadioButtonModule,
    RatingModule,
    SelectButtonModule,
    SidebarModule,
    ScrollerModule,
    ScrollPanelModule,
    ScrollTopModule,
    SkeletonModule,
    SlideMenuModule,
    SliderModule,
    SpeedDialModule,
    SpinnerModule,
    SplitterModule,
    StepperModule,
    SplitButtonModule,
    StepsModule,
    TableModule,
    TabMenuModule,
    TabViewModule,
    TagModule,
    TerminalModule,
    TieredMenuModule,
    TimelineModule,
    ToastModule,
    ToggleButtonModule,
    ToolbarModule,
    TooltipModule,
    TriStateCheckboxModule,
    TreeModule,
    TreeSelectModule,
    TreeTableModule,
    AnimateModule,
    CardModule,
    RippleModule,
    StyleClassModule,
    FloatLabelModule,
    IconFieldModule,
    InputIconModule,
    AutoFocusModule,
    
  ],
 
  providers: [ 
    provideHttpClient(),
    AuthService,
     UserService,
     MessageService,
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
