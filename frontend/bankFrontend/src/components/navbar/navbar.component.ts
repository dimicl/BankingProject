import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import * as AOS from 'aos';
import 'aos/dist/aos.css';


@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
    ngOnInit(): void{
          if (typeof window !== 'undefined' && typeof document !== 'undefined') {
            AOS.init({
              duration: 1000,
              easing: 'ease-in-out',
              once: true
            });
          }
        }

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
  router = inject(Router);
  onSubmit()
  {
    localStorage.removeItem("ime");
    localStorage.removeItem("prezime");
    localStorage.removeItem("pin");
    localStorage.removeItem("brojR");
    localStorage.removeItem("sredstva");
    localStorage.removeItem("valuta");

    this.router.navigateByUrl("login");

  }

}
