import { map } from 'rxjs/operators';
import { HelperService } from './../services/helper.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userLogin = { username: '', password: '' };
  submitted = false;
  passwordType: any;
  password: any;
  loginForm: any
  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService, private helperService: HelperService) { }

  ngOnInit(): void {
    this.validateForm()
  }


  validateForm() {
    this.loginForm = this.formBuilder.group({
      userID: ["", [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]],
      password: ["", [Validators.required, Validators.pattern("^.{8,}")]]
    })
  }

  onLogin() {
    this.submitted = true;
    if (!this.loginForm.valid) {
      console.log(this.loginForm);
    }
    else {
      let url = "User/authenticate";
      let model = {
        "Username": this.loginForm.value.userID,
        "Password": this.loginForm.value.password
      }
      this.authService.post(url, model).then((res) => {
        console.log(res);
        if (res.accessToken != null) {
          localStorage.setItem("accessToken", res.accessToken);
          localStorage.setItem("refreshToken", res.refreshToken);
          this.router.navigate(['app-dashboard']);
          this.authService.isUserLoggedin = true;
        }
      }).catch((err) => {
        console.log(err.status);
        this.helperService.showError(err.error.message)
      })
    }
  }

  get loginFormError() {
    return this.loginForm.controls;
  }
}
