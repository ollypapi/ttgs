import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  constructor(private router : Router) { }

  orders:any = [
    {img:"assets/imgs/droplet.png",name:"001"},
    {img:"assets/imgs/droplet.png",name:"002"},
    {img:"assets/imgs/droplet.png",name:"003"},
    {img:"assets/imgs/droplet.png",name:"004"}
  ]
  
  

  ngOnInit(): void {
    this.mapImageLabel();
  }


  mapImageLabel() {
    let obj = [];    
  let   images = [
      "assets/imgs/droplet.png",
      "assets/imgs/droplet.png",
      "assets/imgs/droplet.png",
      "assets/imgs/droplet.png"
    ]   
 let label = ["application", "type","hi","project"]

    for (let i = 0; i < images.length; i++) {
        obj.push( {label:label[i], url:images[i]})
    }
  console.log(obj)
    return obj;
}



  details(){
    this.router.navigate(["/order-details"]);
  }

}
