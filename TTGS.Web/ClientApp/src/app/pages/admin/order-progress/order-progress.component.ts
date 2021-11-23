import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js'; 

@Component({
  selector: 'app-order-progress',
  templateUrl: './order-progress.component.html',
  styleUrls: ['./order-progress.component.scss']
})
export class OrderProgressComponent implements OnInit {
  chart:any=[]
  constructor() { }

  ngOnInit(): void {
     let chart = new Chart('canvas', {  
      type: "pie",  
      data: {  
        labels:["Recieved","Loaded","Delivered"],  
        datasets: [  
          {  
            data:[50,20,30],  
            borderColor: '#fff',  
            backgroundColor: [  
              "#96c8db" ,
              "#003512" ,
              "#002348"
               
            ],  
            // fill: true  
          }  
        ]  
      },  
      options: {  
        legend: {  
          display: true  
        },  
        onClick: (evt, item) => {
          chart.update()
          // console.log(item[0].active)
          // item[0]._model.outerRadius += 10
        },
      }
            
          
          // var activePoints = chart.getElementsAtEvent(e);
          // var selectedIndex =  activePoints[0]._index;
          // var pieData = chart.data.datasets[0];
          // console.log(selectedIndex)
          // console.log(pieData)

          // alert(chart.data.datasets[0].data[selectedIndex]);
          // }
        // scales: {  
        //   xAxes: [{  
        //     display: true  
        //   }],  
        //   yAxes: [{  
        //     display: true  
        //   }],  
        // }  
      // } , 
    });  
  }

  
  handleClick(){
    var activeElement = this.chart.getElementAtEvent();
    console.log(activeElement)
  }
  

}
