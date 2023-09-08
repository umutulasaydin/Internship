import { Component, OnInit } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-dialog',
  templateUrl: './profile-dialog.component.html',
  styleUrls: ['./profile-dialog.component.css']
})
export class ProfileDialogComponent implements OnInit {
  constructor(private router: Router,private webService: WebService)
  {}

  profile = {
    usId: "",
    usUsername: "",
    usName: "",
    usMail: "",
    usPhoneNum: ""
  };
  message;
  ngOnInit(): void {

    this.webService.UserInfo({}).subscribe(result => {

      if (result.statusCode != 1)
      {
        this.message = result.errorMessage;
      }
      else
      {
        this.profile = result.result;
      }
    })
  }
}
