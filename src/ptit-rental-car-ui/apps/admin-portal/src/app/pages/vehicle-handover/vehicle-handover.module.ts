import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {VehicleHandoverComponent} from './vehicle-handover.component';
import {GridModule} from "@syncfusion/ej2-angular-grids";
import {RouterLink, RouterLinkActive, RouterModule, Routes} from "@angular/router";
import {TextBoxModule} from "@syncfusion/ej2-angular-inputs";
import {DropDownListModule} from "@syncfusion/ej2-angular-dropdowns";
import {ButtonModule} from "@syncfusion/ej2-angular-buttons";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {DatePickerModule} from "@syncfusion/ej2-angular-calendars";

const routes: Routes = [
  {path: 'vehicle-handovers', component: VehicleHandoverComponent, pathMatch: 'full'}
];
@NgModule({
  declarations: [
    VehicleHandoverComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    GridModule,
    RouterLink,
    RouterLinkActive,
    TextBoxModule,
    DropDownListModule,
    ButtonModule,
    ReactiveFormsModule,
    FormsModule,
    DatePickerModule
  ]
})
export class VehicleHandoverModule {
}
