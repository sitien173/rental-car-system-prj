import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CreateDamageAssessment, DamageAssessment, UpdateDamageAssessment} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class DamageAssessmentService {
  private endpoint = 'damage-assessments';

  constructor(private readonly http: HttpClient) {
  }

  getAll() {
    return this.http.get<DamageAssessment[]>(this.endpoint);
  }

  create(request: CreateDamageAssessment) {
    return this.http.post<string>(this.endpoint, request);
  }

  update(method: string, id: string, request: UpdateDamageAssessment) {
    return this.http.request<DamageAssessment>(method, this.endpoint + '/' + id, {
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
