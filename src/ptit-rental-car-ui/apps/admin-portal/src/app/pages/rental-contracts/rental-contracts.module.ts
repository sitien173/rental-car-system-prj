import {NgModule} from '@angular/core';
import {RentalContractComponent} from './rental-contracts.component';
import {RouterModule, Routes} from "@angular/router";
import {GridModule} from "@syncfusion/ej2-angular-grids";
import {DialogModule} from "@syncfusion/ej2-angular-popups";
import {DatePickerModule} from "@syncfusion/ej2-angular-calendars";
import {TextBoxModule} from "@syncfusion/ej2-angular-inputs";
import {AutoLoginPartialRoutesGuard} from "angular-auth-oidc-client";


const routes: Routes = [
  {
    path: 'rental-contracts',
    component: RentalContractComponent,
    canActivate: [AutoLoginPartialRoutesGuard],
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [RentalContractComponent],
  imports: [
    RouterModule.forChild(routes),
    GridModule,
    DialogModule,
    DatePickerModule,
    TextBoxModule
  ],
  providers: [],
})
export class RentalContractsModule {
}
