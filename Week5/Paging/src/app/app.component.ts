import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from './services/api-service.service';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, SortDescriptor } from '@progress/kendo-data-query';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  constructor(private apiService: ApiServiceService) {}
  public gridView: GridDataResult;

  public skip = 0;
  public data: any[] = [];
  maxPage = 0;

  body = {
    clientName: "",
    clientPos: "",
    pageNumber: 1,
    rowsOfPage: 10,
    couponStatus: 0,
    serieId: "",
    serieName: "",
    username: "",
    name: "",
    startOrder: "",
    validOrder: "",
    startDateStart: "1970-01-01",
    startDateEnd: "1970-01-01",
    validDateStart: "1970-01-01",
    validDateEnd: "1970-01-01"
  }

  sort: SortDescriptor[] = [
  ]

  filter: CompositeFilterDescriptor = 
  {
    logic: "and",
    filters: []
  };
  dateCheck: boolean;
  validCheck: boolean;
  format = "MM/dd/yyyy HH:mm:ss";
  start: Date;
  end: Date;

  status = ["","Active", "Used", "Blocked", "Draft"];
  info = [];

  ngOnInit(): void {
    this.loadItems();
  }

  public loadItems()
  {

    this.apiService.GetAllCoupons(this.body).subscribe(x=>
      {
        
        if (x.statusCode != 1)
        {
          this.data = x.errorMessage;
        }
        else
        {
        
          this.maxPage = x.result.maxPage;
          this.data = x.result.data;
          for (let i = 0; i <this.data.length; i++)
          {
            this.data[i].cpnStatus = this.status[this.data[i].cpnStatus]
          }
          this.gridView = {
            data: this.data,
            total: this.maxPage
          }
        }
      });
  }

  public pageChange(event: PageChangeEvent): void
  {

    this.skip = event.skip;
    this.body.pageNumber = event.skip/10 + 1;
    this.loadItems();
  }

  public sortChange(event: SortDescriptor[])
  { 
    if (event[0].field == "cpnStartDate")
    {
      this.sort = event;
      this.body.validOrder = "";
      if (event[0].dir == null)
      {
        this.body.startOrder = "";
      }
      else
      {
        this.body.startOrder = event[0].dir.toLocaleUpperCase();
      }
      
    }
    else if (event[0].field == "cpnValidDate")
    {
      this.sort = event;
      this.body.startOrder = "";
      if (event[0].dir == null)
      {
        this.body.validOrder = "";
      }
      else
      {
        this.body.validOrder = event[0].dir.toLocaleUpperCase();
      }
      
    }
    
    this.loadItems();
  }

  public filterChange(filter: CompositeFilterDescriptor)
  {
    var old = this.filter.filters as Array<any>;
    var filters = filter.filters as Array<any>;
    var missing = old.filter((item) => filters.indexOf(item) < 0);
    console.log(missing);
    if (missing.length >0)
    {
      if (missing[0].field == "cpnStatus")
      {
        this.body.couponStatus = 0;
      }
      else if (missing[0].field == "usUsername")
      {
        this.body.username = "";
      }
      else if (missing[0].field == "usName")
      {
        this.body.name = "";
      }
      else if (missing[0].field == "cpsSeriesId")
      {
        this.body.serieId = "";
      }
      else if (missing[0].field == "cpsSeriesName")
      {
        this.body.serieName = "";
      }
    }
    
    for (let i = 0; i < filters.length; i++)
    {
      
      if (filters[i].field == "cpnStatus")
      {
        this.body.couponStatus = this.status.indexOf(filters[i].value);
      }
      else if (filters[i].field == "usUsername")
      {
        this.body.username = filters[i].value;
      }
      else if (filters[i].field == "usName")
      {
        this.body.name = filters[i].value;
      }
      else if (filters[i].field == "cpsSeriesId")
      {
        this.body.serieId = filters[i].value;
      }
      else if (filters[i].field == "cpsSeriesName")
      {
        this.body.serieName = filters[i].value;
      }
    }
    this.filter = filter;
    this.loadItems();
  }

  public startdateChange(start: Date, end: Date)
  {
    if (start == null && end == null)
    {
      this.body.startDateStart = "1970-01-01";
      this.body.startDateEnd = "1970-01-01";
      this.dateCheck = false;
      
     
    }
    else if (start == null)
    {
      this.body.startDateEnd = formatDate(end, "yyyy-MM-dd", "en-US");
      this.dateCheck = true;
      
    }
    else
    {
      this.body.startDateStart = formatDate(start, "yyyy-MM-dd", "en-US");
      this.dateCheck = true;
      
    }
    
    this.loadItems();
  }

  public validdateChange(start: Date, end: Date)
  {
    if (start == null && end == null)
    {
      this.body.validDateStart = "1970-01-01";
      this.body.validDateEnd = "1970-01-01";
      this.validCheck = false;
    }
    else if (start == null)
    {
      this.body.validDateEnd = formatDate(end, "yyyy-MM-dd", "en-US");
      this.validCheck = true;
    }
    else
    {
      this.body.validDateStart = formatDate(start, "yyyy-MM-dd", "en-US");
      this.validCheck = true;
    }
    
    this.loadItems();
  }
}

