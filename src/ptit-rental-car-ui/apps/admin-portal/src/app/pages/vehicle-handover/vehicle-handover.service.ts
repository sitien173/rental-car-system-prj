import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateVehicleHandover, UpdateVehicleHandover, VehicleHandover} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class VehicleHandoverService {
  private endpoint = 'vehicle-handovers';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<VehicleHandover[]>(this.endpoint);
  }

  create(request: CreateVehicleHandover) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateVehicleHandover) {
    return this.http.request<VehicleHandover>(method, this.endpoint + '/' + id, {
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
