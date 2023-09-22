import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Cartype, CreateCartype, UpdateCartype} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class CartypeService {
  private endpoint = 'car-types';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<Cartype[]>(this.endpoint);
  }

  create(request: CreateCartype) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateCartype) {
    return this.http.request<Cartype>(method, this.endpoint + '/' + id, {
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
}
