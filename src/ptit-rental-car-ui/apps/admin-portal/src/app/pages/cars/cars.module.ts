import {NgModule} from '@angular/core';
import {CarsComponent} from './cars.component';
import {RouterModule, Routes} from "@angular/router";
import {GridModule} from "@syncfusion/ej2-angular-grids";
import {CheckBoxSelectionService, DropDownListModule, MultiSelectAllModule,} from "@syncfusion/ej2-angular-dropdowns";
import {FormsModule,} from "@angular/forms";
import {RichTextEditorModule} from "@syncfusion/ej2-angular-richtexteditor";
import {NgForOf} from "@angular/common";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";

const routes: Routes = [
  {path: 'cars', component: CarsComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [
    CarsComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    GridModule,
    MultiSelectAllModule,
    DropDownListModule,
    RichTextEditorModule,
    CheckBoxAllModule,
    FormsModule,
    NgForOf
  ],
  providers: [],
})
export class CarModule {
}
