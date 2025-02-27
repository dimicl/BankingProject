import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import * as AOS from 'aos';
import 'aos/dist/aos.css';
@Component({
  selector: 'app-footer',
  imports: [RouterLink],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {

}
