import { Component } from '@angular/core';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatCard, MatCardContent, MatCardTitle} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    MatError,
    MatFormField,
    MatCardContent,
    MatCardTitle,
    MatCard,
    MatInput,
    MatLabel,
    MatButton,
    MatIcon
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

}
