import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  apiUrl = "https://localhost:7247/api/";
  constructor(private http: HttpClient) { }

  SignUp(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Signup`, post);
  }

  Login(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Login`, post);
  }

  CreateCoupon(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Create`, post);
  }

  CreateSerie(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Serie`, post);
  }

  Redeem(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Redeem`, post);
  }

  Void(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Void`, post);
  }

  ChangeStatus(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/ChangeStatus`, post);
  }

  GetByUsername(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetUsername`, post);
  }

  GetCouponById(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/Get`, post);
  }

  GetBySeriesId(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetSerieId`, post);
  }

  CouponInfo(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetCouponInfo`, post);
  }

  GetValidCoupons(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetValidCoupons`, post);
  }

  GetAllCoupons(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Coupon/GetAllCoupons`, post);
  }

  GetAllCouponLogs(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}/Coupon/GetAllCouponLogs`, post);
  }

}
