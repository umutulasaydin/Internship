import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WebService } from 'src/app/services/web/web.service';

@Component({
  selector: 'app-create-dialog',
  templateUrl: './create-dialog.component.html',
  styleUrls: ['./create-dialog.component.css']
})
export class CreateDialogComponent {

  constructor(private webService: WebService)
  {}
  message;
  form = new FormGroup({
    cpnRedemptionLimit: new FormControl('', Validators.required),
    cpnStartDate: new FormControl('', Validators.required),
    cpnEndDate: new FormControl('', Validators.required),
    cpnStatus: new FormControl(0)
  });

  

  onSubmit()
  {
    var body = this.form.value
    body.cpnStatus == 0 ? body.cpnStatus=1 : body.cpnStatus=4;
    this.webService.CreateCoupon(body).subscribe(result =>
      {
        if (result.statusCode == 1)
        {
          this.message = "Here is your coupon code: "+result.result.cpnCode;
        }
        else
        {
          this.message = result.errorMessage;
        }
      })
  }
}
