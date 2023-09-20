import {NgModule} from '@angular/core';
import {NgOptimizedImage} from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {GridAllModule} from "@syncfusion/ej2-angular-grids";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";
import {CartypeComponent} from "./cartype.component";


const routes: Routes = [
  {path: 'cartypes', component: CartypeComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [CartypeComponent],
  imports: [
    RouterModule.forChild(routes),
    GridAllModule,
    CheckBoxAllModule,
    NgOptimizedImage
  ],
  providers: [],
  exports: [CartypeComponent]
})
export class CartypeModule {
}
