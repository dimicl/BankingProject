import { NgClass, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [RouterLink, FormsModule, NgClass, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
    korisnik = {
      ime: "",
      prezime:"",
      email: "",
      pin: "",
      isTerms: false
    }
    modalMessage: string = "";
    @ViewChild('exampleModal') modal!: ElementRef;

    http = inject(HttpClient);
    router = inject(Router);
    reset()
    {
      this.korisnik.ime = "";
      this.korisnik.prezime = "";
      this.korisnik.email = "";
      this.korisnik.pin = "";
    }
    onSubmit()
    {
      debugger;
      console.log("Desila se registracija")
      this.http.post("https://localhost:7080/Auth/register", this.korisnik).subscribe((res:any)=>{
        if(res.pinKod)
        {
          this.korisnik.pin = res.pinKod;
          this.modalMessage = `Vas pin je ${this.korisnik.pin}`;
          this.showModal();

        }
        else
          console.log("Greskica");
      })
    }


    showModal() {
      const modalElement = this.modal.nativeElement;
      modalElement.classList.add('show');
      modalElement.style.display = 'block';
      document.body.classList.add('modal-open');
    }

    closeModal() {
      const modalElement = this.modal.nativeElement;
      modalElement.classList.remove('show');
      modalElement.style.display = 'none';
      document.body.classList.remove('modal-open');

      this.router.navigateByUrl("login");
    }

}
