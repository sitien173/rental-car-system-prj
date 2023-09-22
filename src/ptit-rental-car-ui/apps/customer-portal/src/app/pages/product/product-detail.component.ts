import {Component, OnInit} from '@angular/core';
import {CarService} from "@ptit.rentalcar.shared";
import {ActivatedRoute} from "@angular/router";
import {Car} from "@ptit.rentalcar.data-models";

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  providers: [CarService]
})
export class ProductDetailComponent implements OnInit {
  private _carId: string;
  carData: Car;
  constructor(private readonly _carService: CarService,
              private readonly _activatedRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this._activatedRoute.params.subscribe(params => {
      this._carId = params['id'];
    });

    this._carService.getById(this._carId).subscribe(car => {
      this.carData = car;
    });
  }
}
