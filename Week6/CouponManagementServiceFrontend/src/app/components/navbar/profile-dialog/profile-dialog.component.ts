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

  profile: any;

  ngOnInit(): void {

    this.webService.UserInfo({}).subscribe(result => {
      if (result.result == null)
      {
        this.router.navigate([""]);
      }
      else
      {
        this.profile = result.result;
      }
    })
  }
}
