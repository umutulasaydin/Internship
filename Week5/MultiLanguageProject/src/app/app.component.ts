import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Coupon Management Service';

  constructor(private titleService: Title)
  {
    this.titleService.setTitle($localize`${this.title}`);
  }
  localesList = [
    {code: "en", label: "English"},
    {code: "tr", label: "Türkçe"}
  ];
}
