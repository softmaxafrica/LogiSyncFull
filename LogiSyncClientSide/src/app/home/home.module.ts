import { NgModule } from '@angular/core';
 
import { HomeRoutingModule } from './home-routing.module';

import { DividerModule } from 'primeng/divider';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { SplitterModule } from 'primeng/splitter';
import { ToolbarModule } from 'primeng/toolbar';
import { AvatarModule } from 'primeng/avatar';
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { SidebarModule } from 'primeng/sidebar';
import { MenuModule } from 'primeng/menu';
import { MegaMenuModule } from 'primeng/megamenu';
import { MenubarModule } from 'primeng/menubar'; 
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';

 import { MessagesModule } from 'primeng/messages';
 import { SharedModule } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { JobDetailsComponent } from './jobs/job-details/job-details.component';
 
@NgModule({
  declarations: [
   
    
   
   ],
  imports: [
    TagModule,
     HomeRoutingModule,
    CardModule,
    TableModule,
    SidebarModule,
     MessagesModule,
     CommonModule,
    SharedModule,
  ],
  exports:[
   ]
})
export class HomeModule { }
