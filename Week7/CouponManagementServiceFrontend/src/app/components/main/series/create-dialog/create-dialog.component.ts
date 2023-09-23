import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WebService } from 'src/app/services/web/web.service';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent2 {

  constructor(private webService: WebService)
  {}
  message;

  form = new FormGroup({
    cpsSeriesId: new FormControl('', Validators.required),
    cpsSeriesName: new FormControl('', Validators.required),
    cpsSeriesDesc: new FormControl('', Validators.required),
    cpsCount: new FormControl('', Validators.required),
    cpsRedemptionLimit: new FormControl('', Validators.required),
    cpsStartDate: new FormControl('', Validators.required),
    cpsValidDate: new FormControl('', Validators.required),
    cpsStatus: new FormControl(0)
  });

  onSubmit()
  {
    var body = this.form.value
    body.cpsStatus == 0 ? body.cpsStatus=1 : body.cpsStatus=4;
    this.webService.CreateSerie(body).subscribe(result => {
      if (result.statusCode == 1)
        {
          this.message = result.result;
        }
        else
        {
          this.message = result.errorMessage;
        }
    })
  }
}
