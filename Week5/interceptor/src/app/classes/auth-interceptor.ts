import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs";
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor() {}
   
    intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>>
    {
        let newRequest;
        if (request.url.includes("Auth"))
        {
            newRequest = request.clone({headers : new HttpHeaders({
                "x-api-key":"12345"
            })})
        }
        else
        {
            const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidW11dCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InN0cmluZ0BkZW5lbWUuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbW9iaWxlcGhvbmUiOiIwNTU1NTU1NTU1NSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoidW11dHVsYXMiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3ByaW1hcnlzaWQiOiIxIiwiZXhwIjoxNjkyNzczNjI4LCJpc3MiOiJDb3Vwb25NYW5hZ2VtZW50U2VydmljZSIsImF1ZCI6IkNvdXBvbk1hbmFnZW1lbnRTZXJ2aWNlIn0.o1R8Z4sOGn0i8IOAsxocXZ-SfqsCWeI9kYTj4QFjli4";
/*
            const token = localStorage.getItem("token");
        
            if (token != null)
            {
                newRequest.headers.set("token", `Bearer ${token}`);
            }
*/
            newRequest = request.clone({headers : new HttpHeaders({
                
                "x-api-key":"12345", "token":`Bearer ${token}`
            })});
        }

        
        
        return next.handle(newRequest);

    }
}
