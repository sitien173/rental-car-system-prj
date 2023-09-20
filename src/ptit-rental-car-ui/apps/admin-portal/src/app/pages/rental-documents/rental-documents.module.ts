import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";
import {GridAllModule} from "@syncfusion/ej2-angular-grids";
import {RentalDocumentsComponent} from "./rental-documents.component";

const routes: Routes = [
  {path: 'rental-documents', component: RentalDocumentsComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [RentalDocumentsComponent],
  imports: [
    RouterModule.forChild(routes),
    GridAllModule,
    CheckBoxAllModule
  ],
  providers: [],
  exports: [RentalDocumentsComponent]
})
export class RentalDocumentsModule {
}
