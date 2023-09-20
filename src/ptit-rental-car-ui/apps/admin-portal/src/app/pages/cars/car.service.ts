import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Car, CreateCar, UpdateCar} from "@ptit.rentalcar.data-models";
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
