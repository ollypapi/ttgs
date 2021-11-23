import { HelperService } from './helper.service';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, } from "rxjs/operators";
import { Observable, throwError } from 'rxjs';
import { rootUrl } from 'app-config';
@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  private url = rootUrl
  constructor(
    private router: Router,
    private helper: HelperService
  ) {
    console.log(localStorage.getItem("accessToken"));

  }


  interceptor(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err.status === 401) {
          // remove Bearer token and redirect to login page
          this.router.navigate(['/auth/login']);
        }
        return throwError(err);
      })
    );
  }


  intercept(
    httpRequest: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (httpRequest.url.toString().includes(this.url)) {
      const accessToken = localStorage.getItem("accessToken");
      const token = `Bearer ${accessToken}`;
      console.log(token);
      return next
        .handle(httpRequest.clone({ setHeaders: { Authorization: token } }))
        .pipe(
          map((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
              console.log('event--->>>', event);
            }
            return event;
          }),
          catchError(err => {
            // if (err.status === 401) {
            //  // remove Bearer token and redirect to login page
            //   this.router.navigate(["/login"]);



            // }

            switch (err.status) {
              case 401:
                this.router.navigateByUrl("/login");
                break;
              case 403:
                this.router.navigateByUrl("/unauthorized");
                break;
              case 0:
              case 400:
                this.handleAuthError(err);
                break;
              case 405:
              case 406:
              case 409:
              case 500:
                this.handleAuthError(err);
                break;
            }
            return throwError(err);
          })
        );
    }
    return next.handle(httpRequest);
  }


  public handleAuthError(error: any) {
    console.log("error ", error);
    let msg = "";
    if (error !== undefined && typeof error === "string") {
      msg = error;
    } else if (error.error !== undefined && typeof error.error === "string") {
      msg = error.error;
    } else if (
      error.error.error !== undefined &&
      typeof error.error.error === "string"
    ) {
      msg = error.error.error;
    } else {
      msg = error.error.error.errors
        ? error.error.error.errors[0].errorMessage
        : "Something went wrong";
    }
    this.helper.showError(msg);
  }

}
