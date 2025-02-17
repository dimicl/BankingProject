import { NgClass, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
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
      console.log(this.korisnik);
      this.http.post("https://localhost:7080/Auth/register", this.korisnik).subscribe((res:any)=>{
        if(res.pinKod)
        {
          this.korisnik.pin = res.pinKod;
          console.log(`Vas pin je ${this.korisnik.pin}`);
          alert(res.message);
        }
        else
          console.log("Greskica");
      })

      this.router.navigateByUrl("login");

      this.reset();
    }
}
