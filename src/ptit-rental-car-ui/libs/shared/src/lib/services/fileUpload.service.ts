import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {UploadFileResponse} from "@ptit.rentalcar.data-models";

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  constructor(private readonly httpclient: HttpClient) {
  }

  uploadFile(file: File, shouldCreateThumbnail: boolean): Observable<UploadFileResponse> {
    const formData = new FormData();
    formData.append('shouldCreateThumbnail', shouldCreateThumbnail.toString());
    formData.append('file', file);
    return this.httpclient.post<UploadFileResponse[]>('upload', formData).pipe(
      map((response) => {
        return response[0];
      })
    );
  }

  delete(fileName: string) {
    return this.httpclient.delete('upload/' + fileName);
  }
}
