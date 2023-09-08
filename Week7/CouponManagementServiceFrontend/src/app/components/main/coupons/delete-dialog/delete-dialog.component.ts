import { Component } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})
export class DeleteDialogComponent {


  constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>,private webService: WebService)
  {}

  config: any;
  message;

  deleteCoupon()
  {
    this.webService.Delete({input: this.config.id}).subscribe(result => {
      if (result.statusCode != 1)
      {
        this.message = result.errorMessage;
      }
      else
      {
        this.message = result.result;
      }
    });
  }
}
