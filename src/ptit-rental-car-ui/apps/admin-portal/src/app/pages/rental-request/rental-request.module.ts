import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";
import {GridAllModule} from "@syncfusion/ej2-angular-grids";
import {RentalRequestComponent} from "./rental-request.component";

const routes: Routes = [
  {path: 'rental-requests', component: RentalRequestComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [RentalRequestComponent],
  imports: [
    RouterModule.forChild(routes),
    GridAllModule,
    CheckBoxAllModule
  ],
  providers: [],
  exports: [RentalRequestComponent]
})
export class RentalRequestModule {
}
