import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebService {
  apiUrl: string = "https://localhost:7247/api/";
  body: object = {
    "clientName":"string",
    "clientPos":"string"
  };
  constructor(private http: HttpClient) { }

  SignUp(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Signup`, Object.assign(this.body,post), {observe: 'response'});
  }

  Login(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Login`, Object.assign(this.body,post));
  }

  UserInfo(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetUserInfo`, Object.assign(this.body,post));
  }

  Dashboard(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Dashboard`, Object.assign(this.body,post));
  }

  CreateCoupon(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Create`, Object.assign(this.body,post));
  }

  GetAllCoupons(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetAllCoupons`, Object.assign(this.body, post));
  }
}
