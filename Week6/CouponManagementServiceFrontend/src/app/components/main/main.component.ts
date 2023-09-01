import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit{
  token: string;
  constructor(private router: Router)
  {}


  ngOnInit(): void {
    this.token = sessionStorage.getItem("token");
    if (this.token == null)
    {
      this.router.navigate([""]);
    }
    else
    {
     
    }
  }




}
