import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit{
  title = 'ResponsiveLayout';
  
  h1hide = false;



  constructor(public breakpointObserver: BreakpointObserver){}

  ngOnInit(): void {
    this.breakpointObserver.observe(
      ["(min-width: 800px)"]
    )
    .subscribe(result=>
      {
        this.h1hide= false;
        if (result.matches)
        {
          this.h1hide = true;
        }
      });
    
   

    
  }
}
