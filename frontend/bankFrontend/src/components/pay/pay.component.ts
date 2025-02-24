import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pay',
  imports: [FormsModule],
  templateUrl: './pay.component.html',
  styleUrl: './pay.component.css'
})
export class PayComponent {
  forma = {
    SenderAccount : localStorage.getItem("brojR"),
    Iznos : "",
    ReceiverAccount: "",
    Purpose: ""
  };

  
  
  http = inject(HttpClient);
  onSubmit() {
    this.forma.SenderAccount = localStorage.getItem("brojR") || ""; 
    const transferRequest = {
      SenderAccount: this.forma.SenderAccount,
      ReceiverAccount: this.forma.ReceiverAccount,
      Iznos: this.forma.Iznos,
      Svrha: this.forma.Purpose
    };
    debugger;
    this.http.put("https://localhost:7080/Racun/transfer", transferRequest).subscribe((res: any) => {
      if (res.transakcija) {
        console.log("Primljeno", res);
      } else {
        console.log("Greska");
      }
    });
  }
  
}
