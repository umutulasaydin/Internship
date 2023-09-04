import { Component, OnInit } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { SeriesLabelsContentArgs } from "@progress/kendo-angular-charts";
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';

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
  categories = [];
  counts = [];
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
          for (let i = 0; i < this.series.length; i++)
          {
            this.categories.push(this.series[i]["cpsSeriesId"]);
            this.counts.push(this.series[i]["cpsCount"]);
    
          }
          console.log(this.dashboard);
          this.pieCoupon = [
            {category: "Valid Coupon", value: this.dashboard.validCoupon},
            {category: "Unvalid Coupon", value: this.dashboard.totalCoupon-this.dashboard.validCoupon}
          ];
          this.pieStatus = [
            {category: "Active Coupon", value: this.dashboard.active},
            {category: "Blocked Coupon", value: this.dashboard.blocked},
            {category: "Used Coupon", value: this.dashboard.used},
            {category: "Draft Coupon", value: this.dashboard.draft}
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
        { title: 'General Statistics', cols: 1, rows: 1 },
        { title: 'Valid Coupon / Total Coupon', cols: 1, rows: 1 },
        { title: 'Series Info', cols: 1, rows: 1 },
        { title: 'Coupon Status', cols: 1, rows: 1 }
      ];
    })
  );
}
