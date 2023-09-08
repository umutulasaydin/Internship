import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WebService } from 'src/app/services/web/web.service';

@Component({
  selector: 'app-redeem-dialog',
  templateUrl: './redeem-dialog.component.html',
  styleUrls: ['./redeem-dialog.component.css']
})
export class RedeemDialogComponent {

  constructor(private webService: WebService)
  {}
  config: any;
  form = new FormGroup({ value: new FormControl('', Validators.required)});
  message = "";
  onSubmit()
  {
    var value = this.form.value.value;
    
    var body = {
      cpnId: this.config.id,
      amount: value
    }
    this.webService.Redeem(body).subscribe(result =>
      {
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
