import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { LangDialogComponent } from './lang-dialog/lang-dialog.component';
import { ProfileDialogComponent } from './profile-dialog/profile-dialog.component';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  
  current_language: string;
  constructor(private router: Router, private dialog: MatDialog)
  {
    this.current_language = navigator.language;
  }
  
  logout()
  {
    sessionStorage.clear();
    this.router.navigate([""]);
  }

  openLangDialog()
  {
    let dialogRef = this.dialog.open(LangDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      this.current_language = result;
    }); 
  }

  openProfileDialog()
  {
    this.dialog.open(ProfileDialogComponent);
  }
}
