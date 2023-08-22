import { Component } from '@angular/core';
import { ProductService } from './products.service';
import { Observable } from 'rxjs';
import { ColumnMenuSettings, GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { categories } from './data.categories';
import { groupBy, GroupResult } from "@progress/kendo-data-query";

interface Sample {
  interval: number;
  service: string;
  value: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ ProductService ]
})


export class AppComponent {


  
  public dropDownItems = categories;
  public defaultItem = {text: "Filter by Category", value: null};

  public gridItems: Observable<GridDataResult> = new Observable<GridDataResult>();
  public pageSize: number = 10;
  public skip: number = 0;
  public sortDescrpitor: SortDescriptor[] = [];
  public filterTerm: number = 0;
  public menuSettings: ColumnMenuSettings = {
    view: "tabbed",
    filter: true,
  };

  public seriesData = [
    [1, 1, 10], 
    [2, 2, 20], 
    [3, 3, 30]
  ];

  public Data = [
    {
      product: "Chai",
      sales: 200
    },
    {
      product: "Others",
      sales: 250
    }
  ];

  public datas: number[] = [];

  constructor(private service: ProductService)
  {
    this.loadGridItems();
    this.series = groupBy(this.sample, [{ field: "service" }]) as GroupResult[];

    setInterval(() => {
      const variable = this.datas.slice(0);
      variable.push(Math.random());
      if (variable.length> 10)
      {
        variable.shift();
      }

      this.datas = variable;
    }, 100);
  }
  
  public pageChange(event: PageChangeEvent): void
  {
    this.skip = event.skip;
    this.loadGridItems();
  }

  public handleSortChange(descriptor: SortDescriptor[]): void
  {
    this.sortDescrpitor = descriptor;
    this.loadGridItems();
  }

  private loadGridItems(): void
  {
    this.gridItems = this.service.getProducts(
      this.skip,
      this.pageSize,
      this.sortDescrpitor,
      this.filterTerm
    );
  }

  public handleFilterChange(item:any): void {
    this.filterTerm = item.value;
    this.skip = 0;
    this.loadGridItems();
  }

  public sample: Sample[] = [
    {
      interval: 1,
      service: "Service 1",
      value: 5,
    },
    {
      interval: 2,
      service: "Service 1",
      value: 15,
    },
    {
      interval: 3,
      service: "Service 1",
      value: 10,
    },
    {
      interval: 1,
      service: "Service 2",
      value: 10,
    },
    {
      interval: 2,
      service: "Service 2",
      value: 5,
    },
    {
      interval: 3,
      service: "Service 2",
      value: 15,
    },
  ];
  
  public series: GroupResult[];
}
