import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateFeature, Feature, UpdateFeature} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class FeatureService {
  private endpoint = 'features';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<Feature[]>(this.endpoint);
  }

  create(request: CreateFeature) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateFeature) {
    return this.http.request<Feature>(method, this.endpoint + '/' + id, {
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
