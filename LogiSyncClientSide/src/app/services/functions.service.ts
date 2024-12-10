import { Injectable, inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class FunctionsService {
  private messageService = inject(MessageService);
  private translateService = inject(TranslateService);

   private showMessage(severity: string, msgKey: string) {
    this.messageService.clear();
    this.translateService.get(msgKey).subscribe((msg: string) => {
      this.messageService.add({ severity, summary: '', detail: msg });
    });
  }

  // Show error toast
  public displayError(msgKey: any) {
    if (msgKey !== '401') {
      this.showMessage('error', msgKey);
    }
  }

  // Show info toast
  public displayInfo(msgKey: string) {
    this.showMessage('help', msgKey);
  }

  // Show success toast
  public displaySuccess(msgKey: string) {
    this.showMessage('success', msgKey);
  }

  // Specific success messages for insert, update, delete, etc.
  public displayInsertSuccess() {
    this.displaySuccess('msg_insert_success');
  }

  public displayUpdateSuccess() {
    this.displaySuccess('msg_update_success');
  }

  public displayDeleteSuccess() {
    this.displaySuccess('msg_delete_success');
  }

  public displayCancelSuccess() {
    this.displaySuccess('msg_cancel_success');
  }

  public displayProcessSuccess(userMsgKey: string) {
    this.displaySuccess(userMsgKey);
  }

  getSeverity(status: string): "success" | "secondary" | "info" | "warning" | "danger" | "contrast"    | undefined {
    switch (status) {
      case 'CREATED':
        return 'info';
        case 'ON AGREEMENT':
        return 'warning';  
      case 'PENDING':
        return 'warning';  
      case 'COMPLETED':
        return 'success'; 
      case 'APPROVED':
        return 'success';
        case 'PENDING PAYMENTS':
        return 'danger'; 
        //INVOICE

        case 'DRAFT':
          return 'warning';  
            case 'DISCARDED':
        return 'danger';  
      case 'CANCELED':
        return 'danger';  
//payments
        case 'PAID':
        return 'success';
        case 'PARTIAL':
          return 'contrast'; 
          case 'CANCELLED':
            return 'danger'; 
      default:
        return undefined;  
    }
  }
getFormatApiDateOnly(date: string): string {
    const parsedDate = new Date(date);
    // Use the values directly to avoid timezone issues
    const year = parsedDate.getUTCFullYear();
    const month = (parsedDate.getUTCMonth() + 1).toString().padStart(2, '0'); // Month is zero-based
    const day = parsedDate.getUTCDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`; // Returns 'YYYY-MM-DD'
}

getFormatApiDateTime(date: string): string {
    const parsedDate = new Date(date);
    // Use the values directly to avoid timezone issues
    const year = parsedDate.getUTCFullYear();
    const month = (parsedDate.getUTCMonth() + 1).toString().padStart(2, '0'); // Month is zero-based
    const day = parsedDate.getUTCDate().toString().padStart(2, '0');
    const hours = parsedDate.getUTCHours().toString().padStart(2, '0');
    const minutes = parsedDate.getUTCMinutes().toString().padStart(2, '0');
    const seconds = parsedDate.getUTCSeconds().toString().padStart(2, '0');
    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`; // Returns 'YYYY-MM-DDTHH:mm:ss'
}


}
