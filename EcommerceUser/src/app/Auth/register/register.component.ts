import { Component, OnInit } from '@angular/core';
import { MatError, MatFormField, MatLabel } from "@angular/material/form-field";
import { MatCard, MatCardContent, MatCardTitle } from "@angular/material/card";
import { MatInput } from "@angular/material/input";
import { MatButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterLink, RouterOutlet } from "@angular/router";
import { IUserReq } from "../../models/user.model";
import { AccountService } from "../../services/account.service";
import {catchError, first, Observable, tap} from "rxjs";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ImageService } from "../../services/image.service";


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
  selectedFile!: File;
  imageUrl : string = "https://img.freepik.com/free-vector/user-circles-set_78370-4704.jpg?w=740&t=st=1714726785~exp=1714727385~hmac=80009fb6c3a56594a1d9bd8c143258e863866ebbee9eed58972d1d568673b09c";

  constructor(private fb: FormBuilder ,
              private accountService: AccountService ,
              private imageService: ImageService,
              private snackBar: MatSnackBar,
              private router: Router) {
  }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: ["", Validators.required],
      confirmPassword: ["", Validators.required],
      address: ["", Validators.required],
      phone : ["", Validators.required],
    });
  }

  uploadImage(): Observable<any> {
    const uploadData = new FormData();
    uploadData.append('file', this.selectedFile, this.selectedFile.name);
    return this.imageService.UploadImage(uploadData).pipe(
      first(),
      tap(res => {
        this.imageUrl = res.url;
        this.snackBar.open("Image Uploaded", 'Close', {duration: 2000});
      }),
      catchError(error => {
        this.snackBar.open("Error uploading image", "Close", {duration: 3000});
        throw error;
      })
    );
  }

  onSubmit() {
    if (this.selectedFile) {
      this.uploadImage().subscribe(
        () => this.registerUser(),
        error => {
          console.error('Image upload failed', error);
        }
      );
    } else {
      this.registerUser();
    }
  }

  registerUser() {
    const value: IUserReq = {
      name: this.registerForm.value.firstName + " " + this.registerForm.value.lastName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      confirmPassword: this.registerForm.value.confirmPassword,
      address: this.registerForm.value.address,
      mobile: this.registerForm.value.phone,
      image: this.imageUrl
    };
    this.accountService.register(value).subscribe(
      res => {
        this.router.navigate(['/home']);
        console.log(res);
        localStorage.setItem('user', res.data.token.jwt);
        this.snackBar.open("Register successful", "Close", {duration: 3000});
      },
      error => {
        this.snackBar.open(error.error, 'Close', {duration: 2000});
      }
    );
  }

  onFileChanged(event: any) {
    this.selectedFile = event.target.files[0];
  }
}
