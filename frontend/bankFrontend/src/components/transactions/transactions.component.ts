import { DatePipe, JsonPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-transactions',
  imports: [NgFor, JsonPipe, DatePipe],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})
export class TransactionsComponent {

  transakcije: any[] = [];
  pin = localStorage.getItem("pin")?.toString();
  name = localStorage.getItem("ime");
  valuta = localStorage.getItem("valuta");
  http = inject(HttpClient);
  constructor()
  {
    console.log(this.pin);
    this.http.post("https://localhost:7080/Racun/getTransakcije", {Pin : this.pin}).subscribe((res:any)=> {
      if(res.transakcije && res){
        this.transakcije = Array.isArray(res.transakcije) ? res.transakcije : Object.values(res.transakcije);
        console.log("Lista transakcija:", this.transakcije);
        
      }
      else
        console.log("Greska");
    })
  }

 
   
}
