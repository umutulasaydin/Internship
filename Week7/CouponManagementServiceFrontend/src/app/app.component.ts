import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  default_lang = "en-US";
  languages = ["en-US", "tr"]

  
  constructor(private titleService: Title)
  {
    this.titleService.setTitle($localize`CouponManagementService`);
    
  }
  loggedIn: boolean;

  ngOnInit(): void {
    if (sessionStorage.getItem("lang") == null)
    {
      
      if (this.languages.includes(navigator.language))
      {
        sessionStorage.setItem("lang", navigator.language);
        window.location.href = `/${navigator.language}`
      }
      else
      {
        sessionStorage.setItem("lang", "en-US");
        window.location.href = `/${this.default_lang}`
      }
    }
    
    
    if (sessionStorage.getItem("token") != null)
    {
      this.loggedIn = true;
    }
    else
    {
      this.loggedIn = false;
    }
  }
}
