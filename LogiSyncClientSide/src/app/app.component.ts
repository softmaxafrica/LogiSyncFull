import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { SecUser } from './models/SecUser';
import { AuthService } from './auth/auth.service';
import { TranslateService } from '@ngx-translate/core'; // Import translate
import { LanguageLoader } from './services/shared/languageLoader';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl:   './app.component.css', 
})
export class AppComponent implements OnInit {
  title = 'LogiSync';
  items: MenuItem[] = [];
  user: SecUser | null = null; // Store user details for profile pop-up
  profileMenu: any;
  isSidebarVisible: boolean = true;
  isExpanded: boolean = true; // Start with sidebar expanded
  sidebarWidth: string = '200px';

  // Toggle function to expand/collapse the sidebar
 
  constructor(
    private authService: AuthService,
    private router: Router,
    private translate: TranslateService,
    private languageLoader: LanguageLoader // Inject translate
  ) { }

 

  ngOnInit(): void {
    // Load translations first
    this.languageLoader.getTranslation().subscribe(translations => {
      this.translate.setDefaultLang('en'); // Or set the default language based on user preference
      this.translate.use('en'); // Or set the current language based on user preference

      // After translations are loaded, initialize user and menu
      this.authService.loggedInUser$.subscribe((user: SecUser | null) => {
        this.user = user;
        this.updateMenu(user);
      });
    });
  }
  // toggleSidebar() {
  //   this.isExpanded = !this.isExpanded;
  // }
  updateMenu(user: SecUser | null): void {
    if (user) {
      const role = user.role;

      if (role === 'DRIVER') {
        this.items = [
          { label: this.translate.instant('dashboard'), icon: 'assets/images/icons/dashboard.png', routerLink: '/home/dashboard' },
           { label: this.translate.instant('tasks'),icon: 'assets/images/icons/pull-request.png', routerLink: ['/home/drivers/assignments'] },
          { label: this.translate.instant('trucks'), icon: 'assets/images/icons/dashboard.png', routerLink: '/home/trucks' },
          { label: this.translate.instant('logout'), icon: 'assets/images/icons/dashboard.png', command: () => this.logout() }
        ];
      } else if (role === 'COMPANY') {
        this.items = [
          { label: this.translate.instant('dashboard'), icon: 'assets/images/icons/dashboard.png', routerLink: '/home/dashboard' },
          { label: this.translate.instant('invoices'), icon: 'assets/images/icons/invoice.png', routerLink: '/home/billing/invoices' },
          { label: this.translate.instant('payments'), icon: 'assets/images/icons/payment-notification.png', routerLink: '/home/billing/payments' },
          {
            label: this.translate.instant('customers'),icon: 'assets/images/icons/test-drive.png', routerLink: ['/home/customers'],
          },
          {
            label: this.translate.instant('drivers'),icon: 'assets/images/icons/test-drive.png', routerLink: ['/home/drivers/listing'],
          },
          { label: this.translate.instant('trucks'), icon: 'assets/images/icons/tracking-delivery.png', routerLink: '/home/trucks' },
          { label: this.translate.instant('requests'), icon: 'assets/images/icons/pull-request.png', routerLink: '/home/jobs' },
          { label: this.translate.instant('roles'), icon: 'assets/images/icons/dashboard.png', routerLink: ['/home/drivers/roles'] },
        
          { label: this.translate.instant('logout'),icon: 'assets/images/icons/dashboard.png', command: () => this.logout() }
        ];
      }
    } else {
      this.items = [
        { label: this.translate.instant('home'), icon: 'assets/images/icons/home.png', routerLink: '/home/landing' },
        { label: this.translate.instant('login'), icon: 'assets/images/icons/password.png', routerLink: '/account/login' },
        { label: this.translate.instant('register'), icon: 'assets/images/icons/register.png', routerLink: '/account/registration' }
      ];
    }
  }
  onAvatarClick(event: Event): void {
    console.log('Avatar clicked!');
    this.profileMenu.toggle(event);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/account/login']);
  }

  showUserProfile(): void {
    // Method to handle profile pop-up or dialog
  }

  toggleSidebar(): void {
    this.isSidebarVisible = !this.isSidebarVisible;
    this.sidebarWidth = this.isSidebarVisible ? '200px' : '50px';
  }
}
