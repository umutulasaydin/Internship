import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebService {
  apiUrl: string = "https://localhost:7247/api/";
  client: object = {
    "clientName":"string",
    "clientPos":"string"
  };
  constructor(private http: HttpClient) { }

  SignUp(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Signup`, Object.assign(this.client,post), {observe: 'response'});
  }

  Login(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Login`, Object.assign(this.client,post));
  }

  UserInfo(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetUserInfo`, Object.assign(this.client,post));
  }

  Dashboard(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Dashboard`, Object.assign(this.client,post));
  }

  CreateCoupon(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Create`, Object.assign(this.client,post));
  }

  CreateSerie(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Serie`, Object.assign(this.client, post));
  }

  GetAllCoupons(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetAllCoupons`, Object.assign(this.client, post));
  }

  GetAllSeries(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetAllSeries`, Object.assign(this.client, post));
  }

  GetAllLogs(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetAllCouponLogs`, Object.assign(this.client, post));
  }


  Redeem(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Redeem`, Object.assign(this.client,post));
  }

  Void(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Void`, Object.assign(this.client,post));
  }

  Delete(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Delete`, Object.assign(this.client, post));
  }

  ChangeStatus(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/ChangeStatus`, Object.assign(this.client, post));
  }

  DeleteSerie(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/DeleteSerie`, Object.assign(this.client, post));
  }


}
