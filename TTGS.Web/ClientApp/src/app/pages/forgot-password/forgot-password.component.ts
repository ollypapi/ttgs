import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { HelperService } from '../../services/helper.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  submitted = false;
  password:any;
  resetEmailform:any
  constructor(private formBuilder: FormBuilder, private authService : AuthService,private router : Router, private helperService :HelperService) { }

  ngOnInit(): void {
  this.validateForm();
  }

  validateForm(){
    this.resetEmailform = this.formBuilder.group({
      email:["",[Validators.required,Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')]],
    })  
  }


  onsubmit(){
    this.submitted = true;
    if(!this.resetEmailform.valid){
      console.log(this.resetEmailform);
    }
    else{
      let url = "User/forgotpassword";
   let model = { 
       "UserRegisteredEmail":this.resetEmailform.value.email,
     }
     console.log(model);
      this.authService.post(url,model).then((res)=>{
        console.log(res);
          // this.router.navigate(['app-dashboard']);
          this.helperService.showToastr('Message')
           this.authService.isUserLoggedin = true;
    
        // const cookieExists: boolean = this.cookies.check('refreshToken');
        // console.log(cookieExists);
      })
      .catch((err)=>{
        console.log(err.status);
        console.log(err.error);
        // this.helperService.showError(err.error.message)
      })
    }
  }

  get resetEmailformError(){
    return this.resetEmailform.controls;
  }

}
