import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Car, CarItem, CreateCar, UpdateCar} from "@ptit.rentalcar.data-models";
import {map} from "rxjs";
@Injectable({
  providedIn: 'root'
})
export class CarService {
  private endpoint = 'cars';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<Car[]>(this.endpoint);
  }

  getCarsItem() {
    return this.getAll().pipe(
      map(cars => cars.map(car => ({
        id: car.id,
        name: car.name,
        brand: car.brand.name,
        carType: car.carType.name,
        price: car.price,
        image: car.images[0].host + car.images[0].thumbnail
      } as CarItem)
    )));
  }

  create(request: CreateCar) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateCar) {
    return this.http.request<Car>(method, this.endpoint + '/' + id, {
      body: request
    });
  }

  delete(ids: string | string[]) {
    if (typeof ids === 'string') {
      return this.http.delete(this.endpoint + '/' + ids);
    }

    return this.http.delete(this.endpoint, {
      params: {
        ids: ids.join(',')
      }
    });
  }

  getById(carId: string) {
    return this.http.get<Car>(this.endpoint + '/' + carId);
  }
}
