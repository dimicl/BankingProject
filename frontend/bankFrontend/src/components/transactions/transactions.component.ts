import { CommonModule, DatePipe, JsonPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import * as AOS from 'aos';
import 'aos/dist/aos.css';


@Component({
  selector: 'app-transactions',
  imports: [NgFor, DatePipe, CommonModule],
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent {

  transakcije: any[] = [];
  lista : any[] = [];
  sender: any;
  receiver: any;
  pin = localStorage.getItem("pin")?.toString();
  tekuci = localStorage.getItem("brojR");
  valuta = localStorage.getItem("valuta");
  http = inject(HttpClient);

  constructor() {
    var prihodi = 0;
    var rashodi = 0;

    this.http.post("https://localhost:7080/Racun/getTransakcije", { Pin: this.pin }).subscribe((res: any) => {
      if (res.transakcije && res) {
        this.lista = Array.isArray(res.transakcije) ? res.transakcije : Object.values(res.transakcije);
        this.transakcije = Array.isArray(res.transakcije) ? res.transakcije : Object.values(res.transakcije);

        console.log("Transakcije", this.transakcije);

        this.transakcije.forEach((item) => {
          if (item.tip == "Poslato") {
            rashodi += item.iznos;
          } else {
            prihodi += item.iznos;
          }
        });

        localStorage.setItem("prihodi", prihodi.toString());
        localStorage.setItem("rashodi", rashodi.toString());

        this.transakcije = this.transakcije.slice(-10);

        this.transakcije.forEach(item => {
          let tekuciRec = item.tekuciReceiver;
          let tekuciSnd = item.tekuciSender;
          if (tekuciSnd == this.tekuci) {
            item.tip = "Poslato";
            this.http.post("https://localhost:7080/Racun/getReceiver", { tekuciReceiver: tekuciRec }).subscribe((res: any) => {
              if (res.receiver) {
                item.tekuciReceiver = res.receiver.ime;
              } else {
                console.log("Greška");
              }
            });
          } else if (tekuciRec == this.tekuci) {
            item.tip = "Primljeno";
            this.http.post("https://localhost:7080/Racun/getReceiver", { tekuciReceiver: tekuciSnd }).subscribe((res: any) => {
              if (res.receiver) {
                item.tekuciReceiver = res.receiver.ime;
              } else {
                console.log("Greška");
              }
            });
          }
        });
      } else {
        console.log("Greška");
      }
    });
  }

  

  dodajTransakciju(novaTransakcija: any) {
    this.transakcije.push(novaTransakcija);
    if (this.transakcije.length > 10) {
      this.transakcije.shift();  
    }
  }
}
