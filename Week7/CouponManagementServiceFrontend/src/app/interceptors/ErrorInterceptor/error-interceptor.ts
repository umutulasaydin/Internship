import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, catchError, throwError } from "rxjs";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor{


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(err =>{
            if ([401,403,429].includes(err.status) && sessionStorage.getItem("token") != null)
            {
            
                sessionStorage.clear();
            }
            const error = err.error?.message ||err.statusText;
            console.error(error);
            return throwError(() => new Error(err));
        }))
    }
}
