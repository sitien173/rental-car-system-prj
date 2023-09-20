import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const startTime = Date.now();
    let status: string;

    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          status = 'succeeded';
        }
      }, (error) => {
        status = 'failed';
      }),
      tap(() => {
        const elapsedTime = Date.now() - startTime;
        console.log(`${request.method} ${request.urlWithParams} - ${status} [${elapsedTime}ms]`);
      })
    );
  }
}
