import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {OidcSecurityService} from "angular-auth-oidc-client";
import {environment} from "@ptit.rentalcar.app-config";

@Injectable()
export class APIInterceptor implements HttpInterceptor {
  constructor(private readonly oidcSecurityService: OidcSecurityService) {
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let apiUrl = environment.apiUrl;

    if (this.isValidHttpUrl(request.url)) {
      apiUrl = '';
    }
    let accessToken = '';
    this.oidcSecurityService
      .getAccessToken()
      .subscribe((token) => {
        accessToken = token;
      });

    const modifiedRequest = request.clone({
      url: `${apiUrl}${request.url}`,
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    });

    return next.handle(modifiedRequest);
  }

  isValidHttpUrl(input: string) {
    let url;

    try {
      url = new URL(input);
    } catch (_) {
      return false;
    }

    return url.protocol === "http:" || url.protocol === "https:";
  }
}
