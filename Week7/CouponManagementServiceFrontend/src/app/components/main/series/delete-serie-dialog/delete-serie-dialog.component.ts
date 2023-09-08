import { Component } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-serie-dialog',
  templateUrl: './delete-serie-dialog.component.html',
  styleUrls: ['./delete-serie-dialog.component.css']
})
export class DeleteSerieDialogComponent {

  constructor(public dialogRef: MatDialogRef<DeleteSerieDialogComponent>,private webService: WebService)
  {}

  config: any;
  message;

  deleteCoupon()
  {
    this.webService.DeleteSerie({input: this.config.id}).subscribe(result => {
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
