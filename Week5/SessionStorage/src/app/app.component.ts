import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SessionStorage';
  data = "";
  dataSave(value:any)
  {
    sessionStorage.setItem("data", value.data);
  }
  getData()
  {
    var x = sessionStorage.getItem("data");

    if (x == null)
    {
      this.data = "null";
    }
    else
    {
      this.data = x;
    }
  }
}
