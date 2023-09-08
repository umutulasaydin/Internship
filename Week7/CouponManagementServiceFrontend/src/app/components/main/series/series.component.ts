import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { WebService } from 'src/app/services/web/web.service';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { CompositeFilterDescriptor, SortDescriptor } from '@progress/kendo-data-query';
import { formatDate } from '@angular/common';
import { CreateDialogComponent2 } from './create-dialog/create-dialog.component';
import { DeleteSerieDialogComponent } from './delete-serie-dialog/delete-serie-dialog.component';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  styleUrls: ['./series.component.css']
})
export class SeriesComponent implements OnInit {

  constructor(private dialog: MatDialog, private webService: WebService)
  {}

  message;

  public gridView: GridDataResult;
  


  public skip = 0;
  public data: any[] = [];
  maxPage = 0;

  body = {
    pageNumber: 1,
    rowsOfPage: 10,
    id: 0,
    serieId: '',
    insTimeStart: '1970-01-01',
    insTimeEnd: '1970-01-01',
    insOrder: ''
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

  ngOnInit(): void {
    this.loadItems();
  }

  public loadItems()
  {
    this.webService.GetAllSeries(this.body).subscribe(result=>{
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
    this.body.pageNumber = event.skip/this.body.rowsOfPage +1;
    this.loadItems();
  }

  public sortChange(event: SortDescriptor[])
  { 

    if (event[0].field == "cpsInsTime")
    {
      this.sort = event;
      this.body.insOrder = "";
      if (event[0].dir == null)
      {
        this.body.insOrder = "";
      }
      else
      {
        this.body.insOrder = event[0].dir.toLocaleUpperCase();
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
      if (missing[0].field == "cpsId")
      {
        this.body.id = 0;
      }
      else if (missing[0].field == "cpsSeriesId")
      {
        this.body.serieId = "";
      }
    }
    
    for (let i = 0; i < filters.length; i++)
    {
      
      if (filters[i].field == "cpsId")
      {
        this.body.id = filters[i].value;
      }
      else if (filters[i].field == "cpsSeriesId")
      {
        this.body.serieId = filters[i].value;
      }
    }
    this.filter = filter;
    this.loadItems();
  }

  public startdateChange(start: Date, end: Date)
  {
    
    if (start == null && end == null)
    {
      this.body.insTimeStart = "1970-01-01";
      this.body.insTimeEnd = "1970-01-01";
      this.dateCheck = false;
      this.start = null;
      this.end = null;
    }
  
    else if (start == null)
    {
      this.body.insTimeEnd = formatDate(end, this.format, "en-US");
      this.dateCheck = true;
      
    }
    else
    {
      this.body.insTimeStart = formatDate(start, this.format, "en-US");
      this.dateCheck = true;
      
    }
    
    this.loadItems();
  }

  FormatDate(date)
  {
    return formatDate(date, 'yyyy-MM-dd HH:mm:ss', 'en-US');
  }

  createSerie()
  {
    let dialogRef = this.dialog.open(CreateDialogComponent2, {width: '30%', height: '60%'});
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }

  Delete(id)
  {
    let dialogRef = this.dialog.open(DeleteSerieDialogComponent);
    let instance = dialogRef.componentInstance;
    instance.config = {id};
    dialogRef.afterClosed().subscribe(x=>{
      this.loadItems();
    })
  }
}
