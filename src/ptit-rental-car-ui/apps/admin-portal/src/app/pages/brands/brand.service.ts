import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Brand, CreateBrand, UpdateBrand} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private endpoint = 'brands';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<Brand[]>(this.endpoint);
  }

  create(request: CreateBrand) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateBrand) {
    return this.http.request<Brand>(method, this.endpoint + '/' + id, {
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
