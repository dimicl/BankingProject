import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-analytics',
  imports: [FormsModule, NgFor],
  templateUrl: './analytics.component.html',
  styleUrl: './analytics.component.css'
})
export class AnalyticsComponent {
  forma = {
    naziv : "",
    cilj: 0
  }
  pin = localStorage.getItem("pin");
  http = inject(HttpClient);
  lista: any[] = [];
  constructor()
  {
    this.http.post("https://localhost:7080/Stednja/getStednje", {Pin : this.pin}).subscribe((res:any)=>{
      if(res.stednje)
      {
        this.lista = res.stednje;
        console.log("Prikazi", res.stednje);
      }
      else
        console.log("Greska");
    })
  }

  onSubmit()
  {
    const stednja = {
      Pin : this.pin,
      Naziv : this.forma.naziv,
      Cilj : this.forma.cilj
    }
    this.http.post("https://localhost:7080/Stednja/addStednja", stednja).subscribe((res:any)=>{
      if(res.stednja)
        {
          console.log("Prikazi", res.stednja);
          this.forma.naziv = res.stednja.naziv;
          this.forma.cilj = res.stednja.cilj;
          
        }
        
      else
        console.log("Greska");
    })
  }

  

}
