import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  isFormValid(): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;  
  
    return emailRegex.test(this.email.trim()) && 
           this.password.trim() !== '';
  }
  
  email = '';
  password = '';
  messages: { severity: string; summary: string; detail: string }[] = [];

  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  onLogin(): void {
    if (this.email && this.password) {
      this.authService.login(this.email, this.password).subscribe({
        next: () => {
          this.messages = [{
            severity: 'success',
            summary: 'Success',
            detail: 'Welcome again!'
          }];
          setTimeout(() => {
            this.router.navigate(['/home/dashboard']);
          }, 1500); // Delay navigation to allow message display
        },
        error: (err) => {
          this.messages = [{
            severity: 'error',
            summary: 'Login Failed',
            detail: 'Incorrect email or password' 
          }];
        }
      });
    } else {
      this.messages = [{
        severity: 'warn',
        summary: 'Missing Fields',
        detail: 'email and password are required'
      }];
    }
  }
}
