import { CommonModule, CurrencyPipe, NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as AOS from 'aos';
import 'aos/dist/aos.css';

@Component({
  selector: 'app-analytics',
  imports: [FormsModule, NgFor, NgIf, CurrencyPipe, CommonModule],
  templateUrl: './analytics.component.html',
  styleUrl: './analytics.component.css'
})
export class AnalyticsComponent {


  forma = {
    naziv : "",
    cilj: 0,
    vrednost: 0
  }
  pin = localStorage.getItem("pin");
  http = inject(HttpClient);
  lista: any[] = [];
  errorMessage: string = "";
  
  constructor()
  {
    this.http.post("https://localhost:7080/Stednja/getStednje", {Pin : this.pin}).subscribe((res:any)=>{
      if(res.stednje)
      {
        this.lista = res.stednje;

      }
      else
        console.log("Greska");
    })
  }

  selected: string = "";
  goal : number = 0;
  now : number = 0;
  show(item: any)
  {
    this.selected = item.naziv;
    this.goal = item.cilj;
    this.now = item.vrednost;
  }  

  maxStednje : number = 6;
  moguceKreiranje():boolean{
    return this.lista.length < this.maxStednje;
  }


  onSubmit()
  {
    if(!this.moguceKreiranje())
    {
      return;
    }
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
          this.forma.vrednost = res.stednja.vrednost;
          this.lista.push(res.stednja);
          
        }
        
      else
        console.log("Greska", res.response);
    }, (error)=>{
      this.errorMessage = error.error;
    })
    
  }

  dodataVrednost: number = 0;

  dodaj()
  {
    
    const DodajVrednost = {
      Pin : this.pin,
      Naziv : this.selected,
      Iznos : this.dodataVrednost
    };
    this.http.put("https://localhost:7080/Stednja/dodajVrednost", DodajVrednost).subscribe((res:any)=>{
      if(res.vrednost)
        this.now = res.vrednost;
        console.log("Trenutno je", this.now);
      })
  }

  removeStednja()
  {
    const BrisanjeStednje = {
      Pin : this.pin,
      Naziv : this.selected
    };
    this.http.delete(`https://localhost:7080/Stednja/removeStednja`, { body : BrisanjeStednje }).subscribe((res:any)=>{
      console.log("Uspesno", res);
    },
    (error)=> {
      if(error.status == 400)
        console.log("Greska", error.error);
    })
  }
  

  prihodi = Number(localStorage.getItem("prihodi"));
  rashodi = Number(localStorage.getItem("rashodi"));
  valuta = localStorage.getItem("valuta");
  getHeight(type: 'prihodi' | 'rashodi'): string {
    const MAX_HEIGHT = 300; 
    const total = Math.max(this.prihodi, this.rashodi); 
    let height = 0;
    
    if (type === 'prihodi' && total !== 0) {
      height = (this.prihodi / total)* MAX_HEIGHT;  
    } else if (type === 'rashodi' && total !== 0) {
      height = (this.rashodi / total)* MAX_HEIGHT; 
    }

    return height + '%';
  }
}
