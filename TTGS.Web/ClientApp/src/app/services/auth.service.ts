import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { rootUrl } from 'app-config';
// import { JwtHelperService } from '@auth0/angular-jwt';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isUserLoggedin:boolean = false;
  constructor(private http: HttpClient) { }



    post(url: string,reqBody:any): Promise<any> {
        let headers = new HttpHeaders().set("Content-Type", "application/json");
        let apiUrl = `${rootUrl}/${url}`;
        return this.http.post(apiUrl,reqBody, { headers }).toPromise();
    }

  get(url:string): Promise<any> {
    let headers = new HttpHeaders().set("Content-Type", "application/json");
    let apiUrl = `${rootUrl}/${url}`;
    return this.http.get(apiUrl,{ headers }).toPromise();
    }


  update(url:string,reqBody:any): Promise<any> {
    let headers = new HttpHeaders().set("Content-Type", "application/json");
    let apiUrl = `${rootUrl}/${url}`;
    return this.http.put(apiUrl,reqBody,{ headers }).toPromise();
  }

  delete(url:string): Promise<any> {
    let headers = new HttpHeaders().set("Content-Type", "application/json");    
    let apiUrl = `${rootUrl}/${url}`;
    return this.http.delete(apiUrl, { headers }).toPromise();
    }


  //  public  async isAuthenticated(){
  //     let token  =  JSON.parse(localStorage.getItem('token')!);
  //     // Check whether the token is expired and return
  //     // true or false
  //     return !this.jwtHelper.isTokenExpired(token);
  //   }

    isLoggedIn(): boolean {

      return this.isUserLoggedin;
    }
  
}


