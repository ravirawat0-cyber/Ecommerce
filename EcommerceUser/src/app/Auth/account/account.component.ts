import { Component } from '@angular/core';
import {MatDivider} from "@angular/material/divider";
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatIcon} from "@angular/material/icon";
import {MatDialog, MatDialogActions} from "@angular/material/dialog";
import {AccountmodelComponent} from "./accountmodel/accountmodel.component";

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    MatDivider,
    MatCard,
    MatCardContent,
    MatIcon,
    MatDialogActions
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent {


  constructor(private matDialog: MatDialog) {
  }

  openDialog(){
    this.matDialog.open(AccountmodelComponent
    )
  }
}
