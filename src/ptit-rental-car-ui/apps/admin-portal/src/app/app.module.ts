import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import {BrowserModule} from "@angular/platform-browser";
import {BrandModule} from "./pages/brands/brand.module";
import {FeatureModule} from "./pages/features/feature.module";
import {CartypeModule} from "./pages/cartype/cartype.module";
import {ToastrModule} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CommonModule} from "@angular/common";
import {AdditionalFeesModule} from "./pages/additional-fees/additional-fees.module";
import {CarModule} from "./pages/cars/cars.module";
import {RentalDocumentsModule} from "./pages/rental-documents/rental-documents.module";
import {RentalRequestModule} from "./pages/rental-request/rental-request.module";
import {RentalContractsModule} from "./pages/rental-contracts/rental-contracts.module";
import {VehicleHandoverModule} from "./pages/vehicle-handover/vehicle-handover.module";
import {PaymentsModule} from "./pages/payments/payments.module";
import {DamageAssessmentModule} from "./pages/damage-assessments/damage-assessment.module";
import {OidcModule} from "./oidc.module";
import {SharedModule} from "@ptit.rentalcar.shared";
import {HeaderComponent} from "./core/layout/header/header.component";
import {FooterComponent} from "./core/layout/footer/footer.component";
import {SidebarComponent} from "./core/layout/sidebar/sidebar.component";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent
  ],
  imports: [
    ToastrModule.forRoot(),
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    SharedModule,
    BrandModule,
    FeatureModule,
    CartypeModule,
    AdditionalFeesModule,
    CarModule,
    RentalDocumentsModule,
    RentalRequestModule,
    RentalContractsModule,
    VehicleHandoverModule,
    PaymentsModule,
    DamageAssessmentModule,
    VehicleHandoverModule,
    AppRoutingModule,
    OidcModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
