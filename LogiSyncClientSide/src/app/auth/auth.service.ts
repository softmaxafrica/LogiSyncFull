import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { DataService } from '../services/DataService';
import { LoginPayload } from '../models/LoginPayload';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { SecUser } from '../models/SecUser';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {
  private loggedInUserSubject: BehaviorSubject<SecUser | null>;
  public loggedInUser$: Observable<SecUser | null>;

  constructor(private dataService: DataService, private router: Router) {
    const storedUser = this.getLoggedInUserFromStorage();
    this.loggedInUserSubject = new BehaviorSubject<SecUser | null>(storedUser);
    this.loggedInUser$ = this.loggedInUserSubject.asObservable();
  }

  login(email: string, password: string): Observable<SecUser> {
    const payload: LoginPayload = { email, password };

    return this.dataService.login('Login', payload).pipe(
      map((response: ApiResponse<SecUser>) => {
        if (response.success && response.data) {
          return response.data; 
        }
        throw new Error('Login failed'); // Handle failed login
      }),
      tap(user => {
        if (user) {
          sessionStorage.setItem('loggedInUser', JSON.stringify(user));
          this.loggedInUserSubject.next(user); // Set the user into the BehaviorSubject
        }
      })
    );
  }

  logout(): void {
    sessionStorage.removeItem('loggedInUser');
    this.loggedInUserSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  isLoggedIn(): boolean {
    return !!sessionStorage.getItem('loggedInUser');
  }

  getUserRole(): string | null {
    const user = this.getLoggedInUserFromStorage();
    return user?.role ?? null;
  }

  getCompanyId(): string | null {
    const user = this.getLoggedInUserFromStorage();
    return user?.userID ?? null;   
  }

  private getLoggedInUserFromStorage(): SecUser | null {
    const userJson = sessionStorage.getItem('loggedInUser');
    return userJson ? JSON.parse(userJson) : null;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.isLoggedIn();
    if (!isLoggedIn) {
      this.router.navigate(['/account/login']);
    }
    return isLoggedIn;
  }
}
