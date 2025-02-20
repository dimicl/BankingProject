import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink,RouterOutlet],
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
