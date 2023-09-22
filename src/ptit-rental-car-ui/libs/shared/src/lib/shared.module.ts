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
export * from './services/additional-fees.service';
export * from './services/brand.service';
export * from './services/car.service';
export * from './services/cartype.service';
export * from './services/damage-assessment.service';
export * from './services/feature.service';
export * from './services/fileUpload.service';
export * from './services/payment.service';
export * from './services/rental-contracts.service';
export * from './services/rental-documents.service';
export * from './services/rental-request.service';
export * from './services/vehicle-handover.service';

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
