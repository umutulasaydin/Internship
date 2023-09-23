import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateDialogComponent } from './create-dialog/create-dialog.component';
import { WebService } from 'src/app/services/web/web.service';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, SortDescriptor } from '@progress/kendo-data-query';
import { formatDate } from '@angular/common';
import { InfoDialogComponent } from './info-dialog/info-dialog.component';
import { RedeemDialogComponent } from './redeem-dialog/redeem-dialog.component';
import { VoidDialogComponent } from './void-dialog/void-dialog.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { EditDialogComponent } from './edit-dialog/edit-dialog.component';


@Component({
  selector: 'app-coupons',
  templateUrl: './coupons.component.html',
  styleUrls: ['./coupons.component.css']
})
export class CouponsComponent implements OnInit{

  constructor(private dialog: MatDialog, private webService: WebService)
  {}

  message;

  public gridView: GridDataResult;
  


  public skip = 0;
  public data: any[] = [];
  maxPage = 0;

  body = {
    pageNumber: 1,
    rowsOfPage: 20,
    cpnId: 0,
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

  sort: SortDescriptor[] = []

  filter: CompositeFilterDescriptor = {
    logic: "and",
    filters: []
  }

  dateCheck: boolean;
  validCheck: boolean;
  format = "yyyy-MM-dd";
  start: Date;
  end: Date;
  validStart: Date;
  validEnd: Date;

  status = ["",$localize`Active`,$localize`Used`,$localize`Blocked`,$localize`Draft`,$localize`Expired`]


  ngOnInit(): void {
    this.loadItems();
  }

  public loadItems()
  {
    
    this.webService.GetAllCoupons(this.body).subscribe(result =>
      {
 
        if (result.statusCode != 1)
        {
          this.message = result.errorMessage;
        }
        else
        {
          if (this.body.pageNumber > result.result.maxPage/this.body.rowsOfPage +1)
          {
            
            this.skip = result.result.maxPage - (result.result.maxPage % this.body.rowsOfPage);
            this.body.pageNumber = this.skip/this.body.rowsOfPage +1;
            this.loadItems();
            return;
          }
          this.maxPage = result.result.maxPage;
          this.data = result.result.data;
          
          for (let i = 0; i < this.data.length; i++)
          {
            this.data[i].cpnStatus = this.status[this.data[i].cpnStatus];
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
    this.body.pageNumber = event.skip/this.body.rowsOfPage +1;
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
    
    if (missing.length >0)
    {
      if (missing[0].field = "cpnId")
      {
        this.body.cpnId = 0;
      }
      else if (missing[0].field == "usUsername")
      {
        this.body.username = "";
      }
      else if (missing[0].field == "cpsSeriesId")
      {
        this.body.serieId = "";
      }
    }
    
    for (let i = 0; i < filters.length; i++)
    {
      
      if (filters[i].field == "usUsername")
      {
        this.body.username = filters[i].value;
      }
      else if (filters[i].field == "cpsSeriesId")
      {
        this.body.serieId = filters[i].value;
      }
      else if (filters[i].field == "cpnId")
      {
        this.body.cpnId = filters[i].value;
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
      this.start = null;
      this.end = null;
    }
  
    else if (start == null)
    {
      this.body.startDateEnd = formatDate(end, this.format, "en-US");
      this.dateCheck = true;
      
    }
    else
    {
      this.body.startDateStart = formatDate(start, this.format, "en-US");
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
      this.validStart = null;
      this.validEnd = null;
    }
    else if (start == null)
    {
      this.body.validDateEnd = formatDate(end, this.format, "en-US");
      this.validCheck = true;
    }
    else
    {
      this.body.validDateStart = formatDate(start, this.format, "en-US");
      this.validCheck = true;
    }
    
    this.loadItems();
  }


  createCoupon()
  {
    let dialogRef = this.dialog.open(CreateDialogComponent, {width: '50%', height: '40%'});
  }

  statusChange(event)
  {
    this.body.couponStatus = this.status.indexOf(event);
    this.loadItems();
  }

  Info(profile)
  {
    let dialogRef = this.dialog.open(InfoDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {profile};
  }

  Redeem(id)
  {
    let dialogRef = this.dialog.open(RedeemDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {id};
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }

  Void(id)
  {
    let dialogRef = this.dialog.open(VoidDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {id};
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }

  Delete(id)
  {
    let dialogRef = this.dialog.open(DeleteDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {id};
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }

  Edit(id, status)
  {
    let dialogRef = this.dialog.open(EditDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {id: id, status: status};
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }

  FormatDate(date)
  {
    return formatDate(date, 'yyyy-MM-dd HH:mm:ss', 'en-US');
  }
}
