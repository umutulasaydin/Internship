import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebService {
  apiUrl: string = "https://localhost:7247/api/";

  constructor(private http: HttpClient) { }

  SignUp(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Signup`, post, {observe: 'response'});
  }

  Login(post: any): Observable<any>
  {
    return this.http.post<any>(`${this.apiUrl}Auth/Login`, post);
  }
}
