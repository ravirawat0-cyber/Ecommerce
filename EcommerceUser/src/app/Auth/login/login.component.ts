import {Component, OnInit} from '@angular/core';
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatIcon} from "@angular/material/icon";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButton} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import {AccountService} from "../../services/account.service";
import {IUserLoginReq, IUserReq} from "../../models/user.model";
import { Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatCard,
    MatIcon,
    MatCardContent,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    MatButton,
    RouterLink,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private accountService : AccountService, private snackBar: MatSnackBar, private router: Router) {
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ["", Validators.required, Validators.email],
      password : ["", Validators.required],
    })
  }

  onSubmit(form: FormGroup){
    const value : IUserLoginReq = {
      email : this.loginForm.value.email,
      password: this.loginForm.value.password
    }
    this.accountService.login(value).subscribe(
      response =>{
        this.router.navigate(['/home'])
        this.snackBar.open("login successfull", "Close", {
          duration: 3000,
        });
      },
      error =>{
        this.snackBar.open(error.error, 'close' , {
          duration: 2000
        });
      }
    )
  }
;
}
