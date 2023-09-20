import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AdditionalFees, CreateAdditionalFees, UpdateAdditionalFees} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class AdditionalFeesService {
  private endpoint = 'additional-fees';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<AdditionalFees[]>(this.endpoint);
  }

  create(request: CreateAdditionalFees) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateAdditionalFees) {
    return this.http.request<AdditionalFees>(method, this.endpoint + '/' + id, {
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
