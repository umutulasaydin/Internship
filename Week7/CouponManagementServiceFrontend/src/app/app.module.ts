import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoadInterceptor } from './interceptors/LoadingInterceptor/load-interceptor';
import { AuthInterceptor } from './interceptors/AuthInterceptor/auth-interceptor';

import { WebService } from './services/web/web.service';

import { ErrorInterceptor } from './interceptors/ErrorInterceptor/error-interceptor';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { LoaderComponent } from './components/loader/loader.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LangDialogComponent } from './components/navbar/lang-dialog/lang-dialog.component';
import { ProfileDialogComponent } from './components/navbar/profile-dialog/profile-dialog.component';
import { DashboardComponent } from './components/main/dashboard/dashboard.component';
import { InfoDialogComponent } from './components/main/coupons/info-dialog/info-dialog.component';
import { RedeemDialogComponent } from './components/main/coupons/redeem-dialog/redeem-dialog.component';
import { VoidDialogComponent } from './components/main/coupons/void-dialog/void-dialog.component';
import { DeleteDialogComponent } from './components/main/coupons/delete-dialog/delete-dialog.component';
import { EditDialogComponent } from './components/main/coupons/edit-dialog/edit-dialog.component';

import { ChartsModule } from '@progress/kendo-angular-charts';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import 'hammerjs';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { CouponsComponent } from './components/main/coupons/coupons.component';
import { CreateDialogComponent } from './components/main/coupons/create-dialog/create-dialog.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';
import { GridModule } from '@progress/kendo-angular-grid';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { MatSelectModule } from '@angular/material/select';
import { SeriesComponent } from './components/main/series/series.component';
import { LogsComponent } from './components/main/logs/logs.component';
import { CreateDialogComponent2 } from './components/main/series/create-dialog/create-dialog.component';
import { DeleteSerieDialogComponent } from './components/main/series/delete-serie-dialog/delete-serie-dialog.component';





@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent,
    LoginComponent,
    MainComponent,
    NavbarComponent,
    LangDialogComponent,
    ProfileDialogComponent,
    DashboardComponent,
    CouponsComponent,
    CreateDialogComponent,
    InfoDialogComponent,
    RedeemDialogComponent,
    VoidDialogComponent,
    DeleteDialogComponent,
    EditDialogComponent,
    SeriesComponent,
    LogsComponent,
    CreateDialogComponent2,
    DeleteSerieDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDialogModule,
    MatTabsModule,
    ChartsModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSlideToggleModule,
    GridModule,
    DateInputsModule,
    DropDownsModule,
    MatSelectModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoadInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    WebService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
