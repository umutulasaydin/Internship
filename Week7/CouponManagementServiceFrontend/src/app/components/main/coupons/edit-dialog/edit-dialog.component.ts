import { Component } from '@angular/core';
import { WebService } from 'src/app/services/web/web.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent {

  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,private webService: WebService)
  {}

  config: any;
  message;
  selected: number = 0;


  changeStatus()
  {
    if (this.selected != 0)
    {
      var body = {
        status: this.selected,
        cpnId: this.config.id
      };

      this.webService.ChangeStatus(body).subscribe(result => {
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
}
