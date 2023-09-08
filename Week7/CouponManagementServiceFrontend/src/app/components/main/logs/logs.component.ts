import { Component } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, SortDescriptor } from '@progress/kendo-data-query';
import { formatDate } from '@angular/common';


@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent {
  constructor(private webService: WebService)
  {}

  message;

  public gridView: GridDataResult;
  


  public skip = 0;
  public data: any[] = [];
  maxPage = 0;

  body = {
    pageNumber: 1,
    rowsOfPage: 20,
    cplId: 0,
    cplCouponId: 0,
    cplUserId: 0,
    cplOperation: 0,
    username: "",
    name: "",
    dateOrder: "",
    startDate: "1970-01-01",
    endDate: "1970-01-01",
    cplClientName: "",
    cplClientPos: ""
  }

  sort: SortDescriptor[] = []

  filter: CompositeFilterDescriptor = {
    logic: "and",
    filters: []
  }

  dateCheck: boolean;
  format = "yyyy-MM-dd";
  start: Date;
  end: Date;

  operation = ["", $localize`Redeem`, $localize`Void`, $localize`Block`, $localize`Draft`, $localize`Activate`, $localize`Used`, $localize`Expired`]

  ngOnInit(): void {
    this.loadItems();
  }

  public loadItems()
  {
    
    this.webService.GetAllLogs(this.body).subscribe(result =>
      {
        if (result.statusCode != 1)
        {
          this.message = result.errorMessage;
        }
        else
        {
          if (this.body.pageNumber > result.result.maxPage/10 +1)
          {
            
            this.skip = result.result.maxPage - (result.result.maxPage % this.body.rowsOfPage);
            this.body.pageNumber = this.skip/10 +1;
            this.loadItems();
            return;
          }
          this.maxPage = result.result.maxPage;
          this.data = result.result.data;
          for (let i = 0; i < this.data.length; i++)
          {
            this.data[i].cplOperation = this.operation[this.data[i].cplOperation];
          }
          this.gridView = {
            data: this.data,
            total: this.maxPage
          }
        }
      })
      
  }

  public pageChange(event: PageChangeEvent): void
  {
    this.skip = event.skip;
    this.body.pageNumber = event.skip/10 +1;
    this.loadItems();
  }

  public sortChange(event: SortDescriptor[])
  { 

    if (event[0].field == "cplInsTime")
    {
      this.sort = event;
      this.body.dateOrder = "";
      if (event[0].dir == null)
      {
        this.body.dateOrder = "";
      }
      else
      {
        this.body.dateOrder = event[0].dir.toLocaleUpperCase();
      }
      
    }
  
    
    this.loadItems()
  }

  public filterChange(filter: CompositeFilterDescriptor)
  {
    var old = this.filter.filters as Array<any>;
    var filters = filter.filters as Array<any>;
    var missing = old.filter((item) => filters.indexOf(item) < 0);
    
    if (missing.length >0)
    {
      if (missing[0].field == "cplClientName")
      {
        this.body.cplClientName = "";
      }
      else if (missing[0].field == "cplClientPos")
      {
        this.body.cplClientPos = "";
      }
      else if (missing[0].field == "cplCouponId")
      {
        this.body.cplCouponId = 0;
      }
      else if (missing[0].field == "cplId")
      {
        this.body.cplId = 0;
      }
      else if (missing[0].field == "usName")
      {
        this.body.name = "";
      }
      else if (missing[0].field == "usUsername")
      {
        this.body.username = "";
      }
    }
    
    for (let i = 0; i < filters.length; i++)
    {
      
      if (filters[i].field == "cplClientName")
      {
        this.body.cplClientName = filters[i].value;
      }
      else if (filters[i].field == "cplClientPos")
      {
        this.body.cplClientPos = filters[i].value;
      }
      else if (filters[i].field == "cplCouponId")
      {
        this.body.cplCouponId = filters[i].value;
      }
      else if (filters[i].field == "cplId")
      {
        this.body.cplId = filters[i].value;
      }
      else if (filters[i].field == "usName")
      {
        this.body.name = filters[i].value;
      }
      else if (filters[i].field == "usUsername")
      {
        this.body.username = filters[i].value;
      }
    }
    this.filter = filter;

    this.loadItems();
}

public startdateChange(start: Date, end: Date)
  {
    
    if (start == null && end == null)
    {
      this.body.startDate = "1970-01-01";
      this.body.endDate = "1970-01-01";
      this.dateCheck = false;
      this.start = null;
      this.end = null;
    }
  
    else if (start == null)
    {
      this.body.endDate = formatDate(end, this.format, "en-US");
      this.dateCheck = true;
      
    }
    else
    {
      this.body.startDate = formatDate(start, this.format, "en-US");
      this.dateCheck = true;
      
    }
    
    this.loadItems();
  }

  operationChange(event)
  {
    this.body.cplOperation = this.operation.indexOf(event);
    this.loadItems();
  }

  FormatDate(date)
  {
    return formatDate(date, 'yyyy-MM-dd HH:mm:ss', 'en-US');
  }

}
