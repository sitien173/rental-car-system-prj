import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreatePayment, Payment} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private endpoint = 'payments';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<Payment[]>(this.endpoint);
  }

  create(request: CreatePayment) {
    return this.http.post<string>(this.endpoint, request);
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
