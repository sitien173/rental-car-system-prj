import {NgModule} from '@angular/core';
import {BrandsComponent} from "./brands.component";
import {RouterModule, Routes} from "@angular/router";
import {GridModule} from "@syncfusion/ej2-angular-grids";

const routes: Routes = [
  {path: 'brands', component: BrandsComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [BrandsComponent],
  imports: [
    RouterModule.forChild(routes),
    GridModule
  ],
  providers: [],
  exports: [BrandsComponent]
})
export class BrandModule {
}
