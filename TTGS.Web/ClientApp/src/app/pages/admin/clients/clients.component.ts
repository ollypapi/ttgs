import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {
  clients:any = [
    {img:"assets/imgs/droplet.png",name:"C001"},
    {img:"assets/imgs/droplet.png",name:"C002"},
    {img:"assets/imgs/droplet.png",name:"C003"},
    {img:"assets/imgs/droplet.png",name:"C004"},

  ]
  constructor() { }

  ngOnInit(): void {
  }

}
