import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDivider} from "@angular/material/divider";
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatIcon} from "@angular/material/icon";
import {MatDialog, MatDialogActions} from "@angular/material/dialog";
import {AccountmodelComponent} from "./accountmodel/accountmodel.component";
import {IUserRes} from "../../models/user.model";
import {Subscription} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {AccountService} from "../../services/account.service";
import {DatePipe} from "@angular/common";

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
export class AccountComponent implements OnInit, OnDestroy {

  userDetails! : IUserRes
  userSubscritption! : Subscription;
  formatedDate : string | null = "";

  constructor(
    private datePipe: DatePipe,
    private matDialog: MatDialog,
    private snackBar: MatSnackBar,
    private accountService : AccountService) {
  }

  ngOnInit(): void {
        this.loadUser();
    }

  ngOnDestroy(): void {
        this.userSubscritption.unsubscribe();
    }

  loadUser():void {
    this.userSubscritption = this.accountService.user$.subscribe( user => {
      if(user){
        this.userDetails = user;
        this.formatedDate = this.datePipe.transform(this.userDetails.user.joinedDate, 'MMMM d, yyyy');
      }
    });
    this.accountService.loadUserFromToken().subscribe();
  }

  openDialog(){
    this.matDialog.open(AccountmodelComponent
    )
  }
}
