import { NgModule } from '@angular/core';
import {AuthInterceptor, AuthModule, LogLevel} from 'angular-auth-oidc-client';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {environment} from "@ptit.rentalcar.app-config";



@NgModule({
  declarations: [],
  imports: [
    AuthModule.forRoot({
      config: {
        authority: environment.stsUri,
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: 'rental_car_admin_ui_client',
        scope: 'openid profile email rental_car_api offline_access',
        responseType: 'code',
        silentRenewUrl: `${window.location.origin}/silent-renew.html`,
        autoUserInfo: true,
        silentRenew: true,
        useRefreshToken: true,
        triggerRefreshWhenIdTokenExpired: true,
        logLevel: LogLevel.Debug,
      },
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  exports: [AuthModule]
})
export class OidcModule { }


