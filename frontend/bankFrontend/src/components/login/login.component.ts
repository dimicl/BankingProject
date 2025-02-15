import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, inject, QueryList, ViewChildren } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-login',
  imports: [NgFor, CommonModule, RouterLink, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  pin: string[] = ['', '', '', ''];

  @ViewChildren('pinInputs') pinInputs!: QueryList<ElementRef>;

  handleInput(value: string, index: number) {
    this.pin[index] = value.replace(/\D/g, ''); // Dozvoljava samo brojeve

    // Automatski prebacuje fokus na sledeći input
    if (value && index < this.pin.length - 1) {
      setTimeout(() => this.pinInputs.get(index + 1)?.nativeElement.focus(), 10);
    }
  }

  handleKeydown(event: KeyboardEvent, index: number) {
    if (event.key === 'Backspace') {
      event.preventDefault();
      this.pin[index] = ''; // Briše unos u trenutnom polju

      // Vrati fokus na prethodno polje ako postoji
      if (index > 0) {
        setTimeout(() => this.pinInputs.get(index - 1)?.nativeElement.focus(), 10);
      }
    }
  }

  fullPin: string = "";
  onSubmit() {
    this.fullPin = this.pin.join('');
    console.log('Uneti PIN:', this.fullPin);
  }
}
