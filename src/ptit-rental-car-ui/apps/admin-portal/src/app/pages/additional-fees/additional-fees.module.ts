import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {GridAllModule} from "@syncfusion/ej2-angular-grids";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";
import {AdditionalFeesComponent} from "./additional-fees.component";


const routes: Routes = [
  {path: 'additional-fees', component: AdditionalFeesComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [AdditionalFeesComponent],
  imports: [
    RouterModule.forChild(routes),
    GridAllModule,
    CheckBoxAllModule
  ],
  providers: [],
  exports: [AdditionalFeesComponent]
})
export class AdditionalFeesModule {
}
