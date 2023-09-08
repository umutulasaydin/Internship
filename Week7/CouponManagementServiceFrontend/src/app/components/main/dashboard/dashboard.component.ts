import { Component, OnInit } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import '@progress/kendo-angular-intl/locales/tr/all';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{



  error: string;
  dashboard: any;

  pieCoupon;
  pieStatus;
  series: [];
  
  constructor(private breakpointObserver: BreakpointObserver, private webService: WebService)
  {
  }

  ngOnInit(): void {
    this.webService.Dashboard({}).subscribe(result =>
      {
        if (result.statusCode == 1)
        {
          
          this.dashboard = result.result;
          this.series = this.dashboard.series;


          this.pieCoupon = [
            {category: $localize`Valid Coupon`, value: this.dashboard.validCoupon},
            {category: $localize`Unvalid Coupon`, value: this.dashboard.totalCoupon-this.dashboard.validCoupon}
          ];
          this.pieStatus = [
            {category: $localize`Active Coupon`, value: this.dashboard.active},
            {category: $localize`Blocked Coupon`, value: this.dashboard.blocked},
            {category: $localize`Used Coupon`, value: this.dashboard.used},
            {category: $localize`Draft Coupon`, value: this.dashboard.draft},
            {category: $localize`Expired Coupon`, value: this.dashboard.expired}
          ];
          
        }
        else
        {
          this.error = result.errorMessage;
        }
      }) 

  }

  cards = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
    map(({ matches }) => {
      
      return [
        { title: $localize`General Statistics`, cols: 1, rows: 1 },
        { title: $localize`Valid Coupon / Total Coupon`, cols: 1, rows: 1 },
        { title: $localize`Series Info`, cols: 1, rows: 1 },
        { title: $localize`Coupon Status`, cols: 1, rows: 1 }
      ];
    })
  );

  refresh()
  {
    this.ngOnInit();
  }
}
