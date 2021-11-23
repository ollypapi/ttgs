import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private toastr : ToastrService) { }



  showError(msg:any) {
    this.toastr.error(msg,"OPS!",{positionClass: 'toast-bottom-right',});
  }
  showToastr(msg:any){
 this.toastr.error(msg);
  }
  
}
