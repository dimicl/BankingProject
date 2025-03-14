import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import * as AOS from 'aos';
import 'aos/dist/aos.css';


@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  ngAfterViewChecked() {
    AOS.refresh(); 
  }

  ngOnInit() {
    AOS.init({
      duration: 1000,
      easing: 'ease-in-out',
      delay: 100,
      once: true
    });
  }

  user = {
    ime: localStorage.getItem("ime"),
    prezime: localStorage.getItem("prezime"),
    pin : localStorage.getItem("pin"),
    email : localStorage.getItem("email"),
    prijava: localStorage.getItem("prijava")
  }
  racun = {
    brojR : localStorage.getItem("brojR"),
    sredstva: 0,
    valuta : localStorage.getItem("valuta")
  }
  http = inject(HttpClient);
  constructor()
  {
    this.http.post("https://localhost:7080/Racun/getStanje", {Pin : this.user.pin}).subscribe((res:any)=> {
      if(res.sredstva && res)
      {
        this.racun.sredstva = res.sredstva;
        
      }
      else{
        console.log("Greska");
      }
    })
  }
}
