import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
    user = {
      ime: localStorage.getItem("ime"),
      prezime: localStorage.getItem("prezime"),
      pin : localStorage.getItem("pin")
    }
    racun = {
      brojR : localStorage.getItem("brojR"),
      sredstva : Number(localStorage.getItem("sredstva")),
      valuta : localStorage.getItem("valuta")
    }
}
