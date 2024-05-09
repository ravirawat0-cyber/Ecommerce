import { Component } from '@angular/core';
import {MatDialogActions, MatDialogContainer, MatDialogContent} from "@angular/material/dialog";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

@Component({
  selector: 'app-accountmodel',
  standalone: true,
  imports: [
    MatDialogContainer,
    MatDialogActions,
    MatDialogContent,
    MatIconButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatButton,
    MatLabel
  ],
  templateUrl: './accountmodel.component.html',
  styleUrl: './accountmodel.component.css'
})
export class AccountmodelComponent {

}
