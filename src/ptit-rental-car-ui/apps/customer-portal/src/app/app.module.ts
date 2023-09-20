import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import {BrowserModule} from "@angular/platform-browser";
import {OidcModule} from "./oidc.module";
import {AboutComponent} from "./pages/about/about.component";
import {BlogListComponent} from "./pages/blog-list/blog-list.component";
import {BlogDetailComponent} from "./pages/blog-list/blog-detail/blog-detail.component";
import {BlogItemComponent} from "./pages/blog-list/blog-item/blog-item.component";
import {ContactComponent} from "./pages/contact/contact.component";
import {ProductListComponent} from "./pages/product/product-list.component";
import {ProductItemComponent} from "./pages/product/product-item.component";
import {ProductDetailComponent} from "./pages/product/product-detail.component";
import {ServiceComponent} from "./pages/service/service.component";
import {HomeComponent} from "./pages/home/home.component";
import {DatePickerModule, DateTimePickerAllModule} from "@syncfusion/ej2-angular-calendars";
import {HeaderComponent} from "./core/layout/header/header.component";
import {FooterComponent} from "./core/layout/footer/footer.component";
import {SharedModule} from "@ptit.rentalcar.shared";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    AboutComponent,
    BlogListComponent,
    BlogDetailComponent,
    BlogItemComponent,
    ContactComponent,
    ProductListComponent,
    ProductItemComponent,
    ProductDetailComponent,
    HomeComponent,
    ServiceComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    AppRoutingModule,
    OidcModule,
    DatePickerModule,
    DateTimePickerAllModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
