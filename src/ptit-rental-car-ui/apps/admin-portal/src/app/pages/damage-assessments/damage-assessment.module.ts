import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DamageAssessmentComponent } from './damage-assessment.component';
import {RouterModule, Routes} from "@angular/router";
import {GridModule} from "@syncfusion/ej2-angular-grids";

const routes: Routes = [
  {path: 'damage-assessments', component: DamageAssessmentComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [
    DamageAssessmentComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    GridModule
  ]
})
export class DamageAssessmentModule { }
