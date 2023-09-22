import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateRentalDocuments, RentalDocuments, UpdateRentalDocuments} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class RentalDocumentsService {
  private endpoint = 'rental-documents';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<RentalDocuments[]>(this.endpoint);
  }

  create(request: CreateRentalDocuments) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateRentalDocuments) {
    return this.http.request<RentalDocuments>(method, this.endpoint + '/' + id, {
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
