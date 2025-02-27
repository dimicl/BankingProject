import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, ElementRef, inject, QueryList, ViewChildren } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { Router, RouterLink } from '@angular/router';
import { catchError } from 'rxjs';
import { throwError } from 'rxjs';
import * as AOS from 'aos';
import 'aos/dist/aos.css';

@Component({
  selector: 'app-login',
  imports: [NgFor, CommonModule, RouterLink, FormsModule, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  ngOnInit(): void{
    if (typeof window !== 'undefined' && typeof document !== 'undefined') {
      AOS.init({
        duration: 1000,
        easing: 'ease-in-out',
        once: true
      });
    }
  }

  pin: string[] = ['', '', '', ''];
  fullPin: string = "";
  errorMessage: string = "";

  user = {
    ime: " ",
    prezime: " ",
    email: " ",
    pin: " "
  };

  racun = {
    brojR: "",
    sredstva: 0,
    valuta: ""
  }

  @ViewChildren('pinInputs') pinInputs!: QueryList<ElementRef>;


  handleInput(value: string, index: number) {
    this.pin[index] = value.replace(/\D/g, '');

    if (value && index < this.pin.length - 1) {
      setTimeout(() => this.pinInputs.get(index + 1)?.nativeElement.focus(), 10);
    }
  }

  handleKeydown(event: KeyboardEvent, index: number) {
    if (event.key === 'Backspace') {
      event.preventDefault();
      this.pin[index] = ''; 

      if (index > 0) {
        setTimeout(() => this.pinInputs.get(index - 1)?.nativeElement.focus(), 10);
      }
    }
  }

  onSubmit() {
    this.fullPin = this.pin.join('');
    console.log('Uneti PIN:', this.fullPin);
    this.errorMessage = "";
    this.login();
    
  }

  
  http = inject(HttpClient);
  router = inject(Router);
  login() {
    this.user.pin = this.fullPin;
    this.http.post("https://localhost:7080/Auth/login", this.user).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.errorMessage = "Pogrešan PIN"; 
        } else if (error.status === 0) {
          this.errorMessage = "Nije moguće povezivanje sa serverom"; 
        } else {
          this.errorMessage = "Došlo je do greške: " + error.message; 
        }
        return throwError(() => error);
      })
    )
    .subscribe((res: any) => {
        if (res.korisnik) {
          this.user.ime = res.korisnik.ime;
          this.user.prezime = res.korisnik.prezime;
          this.user.email = res.korisnik.email;
          this.racun.brojR = res.racun.brojRacuna;
          this.racun.sredstva = res.racun.sredstva;
          this.racun.valuta = res.racun.valuta;
          var datum = new Date().toLocaleString();
          console.log("Uspešan login:", this.user);
          localStorage.setItem("ime", this.user.ime);
          localStorage.setItem("prezime", this.user.prezime);
          localStorage.setItem("email", this.user.email);
          localStorage.setItem("pin", this.user.pin);
          localStorage.setItem("brojR", this.racun.brojR);
          localStorage.setItem("sredstva", this.racun.sredstva.toString());
          localStorage.setItem("valuta", this.racun.valuta);
          localStorage.setItem("prijava", datum);

          this.router.navigateByUrl("dashboard");
        } 
        else {
          this.errorMessage = "Invalid PIN";
        }
      });
  }
}
