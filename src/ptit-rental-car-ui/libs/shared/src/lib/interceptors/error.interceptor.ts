import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {ToastrService} from "ngx-toastr";
import {OidcSecurityService} from "angular-auth-oidc-client";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private readonly toastr: ToastrService,
              private readonly oidcSecurityService: OidcSecurityService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 403) {
          this.toastr.error('You are not authorized to perform this action.', 'Error');
          return throwError(() => error);
        }
        if (error.status === 404) {
          this.toastr.error('The resource you are looking for was not found.', 'Error');
          return throwError(() => error);
        }
        if (error.status === 0) {
          this.toastr.error('Server is not responding. Please try again later.', 'Error');
          return throwError(() => error);
        }

        if (error.status === 401) {
          // Unauthorized error, initiate token refresh
          this.oidcSecurityService
            .checkAuth()
            .subscribe( (authenticated) => {
              if (!authenticated.isAuthenticated) {
                this.oidcSecurityService.authorize();
              }
            });

          return throwError(() => error);
        }

        if (error.status === 400) {
          this.toastr.error(error.error.detail, 'Bad request');
          const errors = error.error.extensions.errors;
          for (const key in errors) {
            for (const err of errors[key]) {
              this.toastr.error(err, 'Error');
            }
          }
          return throwError(() => error);
        }
        return throwError(() => error);
      })
    );
  }
}
