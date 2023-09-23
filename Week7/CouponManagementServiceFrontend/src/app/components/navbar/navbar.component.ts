import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
  }
  
  logout()
  {
    sessionStorage.removeItem("token");
    window.location.href = `/${sessionStorage.getItem('lang')}`;
  }

  openLangDialog()
  {
    let dialogRef = this.dialog.open(LangDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result != null || result != undefined)
      {
        sessionStorage.setItem("lang", result);
      window.location.href = `/${result}`;
      }
      
    }); 
  }

  openProfileDialog()
  {
    this.dialog.open(ProfileDialogComponent);
  }
}
