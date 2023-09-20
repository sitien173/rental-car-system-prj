import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentsComponent } from './payments.component';
import {RouterModule, Routes} from "@angular/router";
import {GridModule} from "@syncfusion/ej2-angular-grids";

const routes: Routes = [
  {path: 'payments', component: PaymentsComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [
    PaymentsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    GridModule
  ]
})
export class PaymentsModule { }
