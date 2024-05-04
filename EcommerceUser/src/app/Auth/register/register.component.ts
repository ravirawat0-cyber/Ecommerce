import {Component, OnInit} from '@angular/core';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatCard, MatCardContent, MatCardTitle} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {RouterLink, RouterOutlet} from "@angular/router";
import {IUserReq} from "../../models/user.model";
import {AccountService} from "../../services/account.service";
import {first} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";


@Component({
  imports: [
    MatError,
    MatFormField,
    MatCardContent,
    MatCardTitle,
    MatCard,
    MatInput,
    MatLabel,
    MatButton,
    MatIcon,
    ReactiveFormsModule,
    RouterOutlet,
    RouterLink,
  ],

  selector: 'app-register',
  standalone: true,
  styleUrl: './register.component.css',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;


  constructor(private fb: FormBuilder , private accountService: AccountService , private snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      email: ["", Validators.required, Validators.email],
      password: ["", Validators.required],
      confirmPassword: ["", Validators.required],
      address: ["", Validators.required],
      phone : ["", Validators.required],
    })
  }


  onSubmit(form: FormGroup){
    const value : IUserReq = {
      name : this.registerForm.value.firstName +" "+ this.registerForm.value.lastName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      confirmPassword: this.registerForm.value.confirmPassword,
      address: this.registerForm.value.address,
      mobile: this.registerForm.value.phone
    }
    this.accountService.register(value).subscribe(
      res => {
        console.log(res);
        localStorage.setItem('user', res.data.token.jwt);
        this.snackBar.open("register successfull", "Close", {
          duration: 2000,
        });
      },
      error => {
        this.snackBar.open(error.error, 'close', {
          duration: 2000,
        })
      }
    )


  }
}


