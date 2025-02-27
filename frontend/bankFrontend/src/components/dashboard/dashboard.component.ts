import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { FooterComponent } from '../footer/footer.component';
import * as AOS from 'aos';
import 'aos/dist/aos.css';


@Component({
  selector: 'app-dashboard',
  imports: [RouterLink,RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
   ngOnInit(): void{
      if (typeof window !== 'undefined' && typeof document !== 'undefined') {
        AOS.init({
          duration: 1000,
          easing: 'ease-in-out',
          once: true
        });
      }
    }
    
}
