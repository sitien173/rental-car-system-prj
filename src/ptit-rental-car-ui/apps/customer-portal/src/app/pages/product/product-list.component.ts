import {Component, OnInit} from '@angular/core';
import {CarService} from "@ptit.rentalcar.shared";
import {CarItem} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  providers: [CarService]
})
export class ProductListComponent implements OnInit {
  constructor(private readonly _carService: CarService) {
  }

  cars: CarItem[] = [];

  ngOnInit() {
    this._carService.getCarsItem().subscribe(cars => {
      this.cars = cars;
    });
  }
}
