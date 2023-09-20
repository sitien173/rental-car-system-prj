import {FeatureComponent} from "./feature.component";
import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {CheckBoxAllModule} from "@syncfusion/ej2-angular-buttons";
import {GridAllModule} from "@syncfusion/ej2-angular-grids";

const routes: Routes = [
  {path: 'features', component: FeatureComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [FeatureComponent],
  imports: [
    RouterModule.forChild(routes),
    GridAllModule,
    CheckBoxAllModule
  ],
  providers: [],
  exports: [FeatureComponent]
})
export class FeatureModule {
}
