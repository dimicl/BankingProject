import { CommonModule, DatePipe, JsonPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-transactions',
  imports: [NgFor, DatePipe, CommonModule],
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent {

  transakcije: any[] = [];
  sender: any;
  receiver: any;
  pin = localStorage.getItem("pin")?.toString();
  tekuci = localStorage.getItem("brojR");
  valuta = localStorage.getItem("valuta");
  http = inject(HttpClient);

  constructor() {
    this.http.post("https://localhost:7080/Racun/getTransakcije", { Pin: this.pin }).subscribe((res: any) => {
      if (res.transakcije && res) {
        this.transakcije = Array.isArray(res.transakcije) ? res.transakcije : Object.values(res.transakcije);
        this.transakcije.forEach(item => {
          let tekuciRec = item.tekuciReceiver;
          let tekuciSnd = item.tekuciSender;
          if (tekuciSnd == this.tekuci) {
            item.tip = "Poslato ";
            this.http.post("https://localhost:7080/Racun/getReceiver", {tekuciReceiver: tekuciRec}).subscribe((res: any) => {
              if (res.receiver)
              {
                item.tekuciReceiver = res.receiver.ime;
              }
               else
                console.log("Greška");
            });
          } 
          else if(tekuciRec == this.tekuci) 
          {
            item.tip = "Primljeno";
            this.http.post("https://localhost:7080/Racun/getReceiver", {tekuciReceiver: tekuciSnd}).subscribe((res: any) => {
              if (res.receiver)
              {
                item.tekuciReceiver = res.receiver.ime;
              }
               else
                console.log("Greška");
            });
          }
        });
      } else {
        console.log("Greška");
      }
    });
  }
}
