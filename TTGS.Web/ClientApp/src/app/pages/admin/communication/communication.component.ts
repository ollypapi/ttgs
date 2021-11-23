import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-communication',
  templateUrl: './communication.component.html',
  styleUrls: ['./communication.component.scss']
})
export class CommunicationComponent implements OnInit {
  communicationForm:any;
  submitted:any = false;
  messages = [
    {
      // user:"Uche",
      createdAt:1588487638929,
      msg:"Hi, I'm Uche, what's your name?"
    },
  ]
  agentResponse = [{
    // user:"Uche",
    createdAt:new Date().getTime(),   
    msg:"How can I assist you?",
  }]
  currentUser="me"
  txtmsg:string = ""
  constructor(private authService: AuthService,private router : Router,private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.validateForm();
  }


  validateForm(){
    this.communicationForm = this.formBuilder.group({
      name:["",[Validators.required]],
      email:["",[Validators.required]],
      subject:["",[Validators.required]],
      message:["",[Validators.required]]

    })
    
  }

  send(){
    this.submitted = true;
    if(!this.communicationForm.valid){
      console.log(this.communicationForm);
    }
    else{
     let url = "User/authenticate";
   let model = { 
       "Name":this.communicationForm.value.name,
       "Email":this.communicationForm.value.email,
       "Subject":this.communicationForm.value.subject,
       "Message":this.communicationForm.value.message


     }
      this.authService.post(url,model).then((res)=>{
        console.log(res);
        // if(res){
        //   this.router.navigate(['app-dashboard'])
        // }
      })
    }
  }


  get comunicationFormError(){
    return this.communicationForm.controls;
    
  }

}
