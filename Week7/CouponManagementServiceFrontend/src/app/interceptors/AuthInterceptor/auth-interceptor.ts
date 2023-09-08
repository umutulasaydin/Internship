import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = sessionStorage.getItem("token");
        const lang = sessionStorage.getItem("lang") == "tr" ? "tr-TR" : "";
        let newReq = req.clone({headers: new HttpHeaders({
            "x-api-key":"12345",
            "Accept-Language":lang,
            "token":`Bearer ${token}`
        })});
 

        return next.handle(newReq);
    }
}
