import { Component } from '@angular/core';
import {MatDivider} from "@angular/material/divider";
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    MatDivider,
    MatCard,
    MatCardContent,
    MatIcon
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent {

}
