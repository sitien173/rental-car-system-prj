import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RentalRequest} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class RentalRequestService {
  private readonly endpoint = "rental-requests";

  constructor(
    private readonly http: HttpClient
  ) {
  }

  getAll() {
    return this.http.get<RentalRequest[]>(this.endpoint);
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

  update(method: string, id: string, request: any) {
    return this.http.request<RentalRequest>(method, this.endpoint + '/' + id, {
      body: request
    });
  }
}
