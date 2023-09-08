import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-info-dialog',
  templateUrl: './info-dialog.component.html',
  styleUrls: ['./info-dialog.component.css']
})
export class InfoDialogComponent implements OnInit{
  config: any;
  profile: any;
  ngOnInit(): void {
    this.profile = this.config.profile;

  }
}
