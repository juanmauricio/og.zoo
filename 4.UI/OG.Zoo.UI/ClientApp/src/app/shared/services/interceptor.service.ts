import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/login/services/auth.service';

@Injectable()
export class InterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService, private snackBar: MatSnackBar, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let custom = req.clone();
    if (req.method === 'GET') {
      custom = req.clone({
        headers: req.headers.set('Cache-Control', 'no-cache').set('Pragma', 'no-cache')
      });
    }

    return next.handle(custom).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.authService.deauthenticate();
          this.router.navigate(['/auth']);
          this.snackBar.open('Auth denied.');
        }
        return throwError(error);
      }));
  }
}
