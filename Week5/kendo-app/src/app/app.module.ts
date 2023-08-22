import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ChartsModule } from '@progress/kendo-angular-charts';
import 'hammerjs';







@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    DateInputsModule,
    BrowserAnimationsModule,
    GridModule,
    DropDownsModule,
    ChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
