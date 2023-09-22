import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateRentalContract, RentalContract, UpdateRentalContract} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class RentalContractsService {
  private endpoint = 'rental-contracts';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<RentalContract[]>(this.endpoint);
  }

  create(request: CreateRentalContract) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateRentalContract) {
    return this.http.request<RentalContract>(method, this.endpoint + '/' + id, {
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
