import {Component, Input} from '@angular/core';
import {CarItem} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {
  @Input()
  car: CarItem;
}
