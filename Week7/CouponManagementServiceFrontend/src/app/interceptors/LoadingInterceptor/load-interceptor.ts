import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { LoaderService } from "src/app/services/loader/loader.service";
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';


@Injectable()
export class LoadInterceptor implements HttpInterceptor {

    constructor(private loaderService: LoaderService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loaderService.show();

        return next.handle(req).pipe(
            finalize(() => this.loaderService.hide()),
        );
    }
}
