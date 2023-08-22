import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiServiceService } from './services/api-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'interceptor';
  data = "";
  constructor (private http: HttpClient, private apiService: ApiServiceService) {}

  ngOnInit() {

    const body = {
      username: "umutulas",
      password: "umut6262",
      clientName: "string",
      clientPos: "string",
    };
    this.apiService.Login(body).subscribe( x=>
      {
        if (x.statusCode != 1)
        {
          this.data = x.errorMessage; 
        }
        else
        {
          this.data = x.result;
        }
        console.log(x);
      }
    )
  }

}

