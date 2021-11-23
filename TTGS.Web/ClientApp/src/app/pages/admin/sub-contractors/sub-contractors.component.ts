import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sub-contractors',
  templateUrl: './sub-contractors.component.html',
  styleUrls: ['./sub-contractors.component.scss']
})
export class SubContractorsComponent implements OnInit {
  subContractors:any = [
    {img:"assets/imgs/droplet.png",name:"SC001"},
    {img:"assets/imgs/droplet.png",name:"SC002"},
    {img:"assets/imgs/droplet.png",name:"SC003"},
    {img:"assets/imgs/droplet.png",name:"SC004"},
    {img:"assets/imgs/droplet.png",name:"SC005"},
    {img:"assets/imgs/droplet.png",name:"SC006"},

  ]
  constructor() { }

  ngOnInit(): void {
  }

}
