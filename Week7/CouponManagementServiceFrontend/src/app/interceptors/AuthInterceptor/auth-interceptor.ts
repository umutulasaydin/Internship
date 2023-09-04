import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let newReq = req.clone({headers: new HttpHeaders({
            "x-api-key":"12345"
        })});
 
        if (req.url.includes("Auth") == false)
        {
            const token = sessionStorage.getItem("token");
            if (token != null)
            {
                newReq = req.clone({headers: new HttpHeaders({
                    "x-api-key":"12345",
                    "token":`Bearer ${token}`
                })});
            }
        }
        return next.handle(newReq);
    }
}
