import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WebService } from 'src/app/services/web/web.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{


  constructor(private webService: WebService, private router: Router){}
  
  ngOnInit(): void {
    if (sessionStorage.getItem("token") != null)
    {
      this.router.navigate([""])
    }
  }


  //Form
  loginForm = new FormGroup({
    username: new FormControl('', [
      Validators.required, Validators.minLength(5)
    ]),
    password: new FormControl('', [
      Validators.required, Validators.minLength(8)
    ])
  })

  get username()
  {
    return this.loginForm.get("username");
  }

  get password()
  {
    return this.loginForm.get("password");
  }

  //login result
  message = null;
  fail = false;

  //login
  login()
  {

    var body = {
      username: this.username.value,
      password: this.password.value,
    };
    this.webService.Login(body).subscribe(x=>{
      if(x.statusCode == "1")
      {
        sessionStorage.setItem("token", x.result);
        this.message = x.result;

        window.location.reload();
      }
      else
      {
        this.message = x.errorMessage;
        this.fail = true;
     
      }
      this.loginForm.reset();
    })
  }

  
}
