import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialogActions, MatDialogContainer, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {IUserRes, IUserUpdateReq} from "../../../models/user.model";
import {Subscription} from "rxjs";
import { AccountService } from '../../../services/account.service';
import {MatSnackBar} from "@angular/material/snack-bar";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";

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
    MatLabel,
    ReactiveFormsModule
  ],
  templateUrl: './accountmodel.component.html',
  styleUrl: './accountmodel.component.css'
})
export class AccountmodelComponent implements  OnInit, OnDestroy{

  userDetail ! : IUserRes;
  userSubscription! : Subscription;
  registerForm! : FormGroup;

  constructor(private accountService : AccountService,
              private snackBar : MatSnackBar,
              private fb: FormBuilder,
              public dialogRef: MatDialogRef<AccountmodelComponent>) {
  }
   ngOnInit(): void {
       this.registerForm = this.fb.group({
         firstName : ["", Validators.required],
         lastName : ["", Validators.required],
         address : ["", Validators.required],
         phone : ["", Validators.required],
       })
       this.loadUser();
   }
   ngOnDestroy(): void {
       this.userSubscription.unsubscribe();
   }

  loadUser(): void {
    this.userSubscription = this.accountService.user$.subscribe(user => {
      if (user) {
        this.userDetail = user;
      }
    });
    this.accountService.loadUserFromToken().subscribe();
  }

  updateUserDetail() {
     const value : IUserUpdateReq = {
       name : this.registerForm.value.firstName + " " + this.registerForm.value.lastName,
       mobile : this.registerForm.value.phone,
       address : this.registerForm.value.address,
     }
     this.accountService.updateUser(value).subscribe(
       res => {
         this.loadUser();
         this.snackBar.open("user details updated", "Close", {duration: 3000})
       },
       error => {
         this.snackBar.open("error updating details", "Close" , {duration : 3000})
       }
     )
  }

  protected readonly close = close;
}
