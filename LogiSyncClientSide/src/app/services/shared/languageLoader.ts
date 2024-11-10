import { Injectable } from '@angular/core';
import { TranslateLoader, TranslateService } from '@ngx-translate/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AppConstants } from './Constants';

@Injectable({
  providedIn: 'root', // This ensures it is available app-wide
})
export class LanguageLoader {
  private _urlBase: string;

  constructor(private http: HttpClient,private translate: TranslateService) {
    this._urlBase = AppConstants.API_BASE_URL;

  }

 

  getTranslation(): Observable<any> {
    const url = `${AppConstants.API_BASE_URL}Translation/GetGenEngTranslations`;
    const headers = this.getHeaders();

    // Check if translations are already stored in sessionStorage
    const storedTranslations = sessionStorage.getItem('translation');
    if (storedTranslations) {
      return new Observable(observer => {
        observer.next(JSON.parse(storedTranslations));
        observer.complete();
      });
    } else {
      return this.http.get<any>(url, { headers })
        .pipe(
          map(response => {
            if (response.success && response.data) {
              // Store translations in sessionStorage
              sessionStorage.setItem('translation', response.data);
              return JSON.parse(response.data);
            }
            throw new Error('Failed to load translations');
          }),
          catchError(this.handleError)
        );
    }
  }

  private getHeaders(): HttpHeaders {
    return new HttpHeaders()
      .set('Content-Type', 'application/json; charset=utf-8')
      .set('Accept', 'application/json');
  }

  private handleError(error: Error | HttpErrorResponse) {
    let errMsg: string;
    if (error instanceof HttpErrorResponse) {
      switch (error.status) {
        case 400:
          errMsg = error.error;
          break;
        case 401:
        case 403:
          errMsg = `401`;
          break;
        default:
          errMsg = error.error;
          break;
      }
    } else {
      errMsg = `An error occurred: ${error.message}`;
      alert(errMsg);
    }

    return throwError(errMsg);
  }
}
