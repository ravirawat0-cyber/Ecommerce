import { Component } from '@angular/core';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-login-required',
  standalone: true,
    imports: [
        NgIf
    ],
  templateUrl: './login-required.component.html',
  styleUrl: './login-required.component.css'
})
export class LoginRequiredComponent {

}
