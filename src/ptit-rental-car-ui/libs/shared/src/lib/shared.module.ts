import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {LoggingInterceptor} from "./interceptors/logging.interceptor";
import {CorsInterceptor} from "./interceptors/cors.interceptor";
import {ErrorInterceptor} from "./interceptors/error.interceptor";
import {SpinnerComponent} from "./components/spinner/spinner.component";
import {PageNotFoundComponent} from "./components/page-not-found/page-not-found.component";
import {VNDCurrencyPipe} from "./pipes/vndcurrency.pipe";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {APIInterceptor} from "./interceptors/api.interceptor";
export * from './services/fileUpload.service';
export * from './components/spinner/spinner.component';
export * from './components/page-not-found/page-not-found.component';
export * from './pipes/vndcurrency.pipe';
export * from './validations/PasswordValidator';
export * from './validations/EqualValidator';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    HttpClientModule
  ],
  declarations: [
    SpinnerComponent,
    PageNotFoundComponent,
    VNDCurrencyPipe
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoggingInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: APIInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CorsInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  exports: [
    CommonModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SpinnerComponent,
    PageNotFoundComponent,
    VNDCurrencyPipe
  ]
})
export class SharedModule {}
