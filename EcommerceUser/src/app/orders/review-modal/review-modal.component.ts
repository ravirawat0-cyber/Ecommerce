import {Component, Inject, OnInit} from '@angular/core';

import {MatIcon} from "@angular/material/icon";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {StarRatingComponent} from "../../global/star-rating/star-rating.component";
import {MatInput} from "@angular/material/input";
import {MAT_DIALOG_DATA, MatDialogContainer, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {ReviewService} from "../../services/review.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IReviewReq} from "../../models/review.model";

@Component({
  selector: 'app-review-modal',
  standalone: true,
  imports: [
    MatIcon,
    MatIconButton,
    MatFormField,
    StarRatingComponent,
    MatInput,
    MatButton,
    MatLabel,
    MatDialogContent,
    MatDialogContainer,
    ReactiveFormsModule
  ],
  templateUrl: './review-modal.component.html',
  styleUrl: './review-modal.component.css'
})
export class ReviewModalComponent implements  OnInit{
  reviewForm ! : FormGroup;
  productId!: number;

  constructor(public dialogRef : MatDialogRef<ReviewModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private fb: FormBuilder,
              private reviewService: ReviewService,
              private snackbar : MatSnackBar) {

    {this.productId = data.productId}
  }

  ngOnInit(): void {
        this.reviewForm = this.fb.group({
          reviewNumber : [1, Validators.required],
          review : ["", Validators.required],
        })
    }

  addReview() {
       const req : IReviewReq = {
         rating : this.reviewForm.value.reviewNumber,
         comments : this.reviewForm.value.review,
         productId : this.productId,
       }
       this.reviewService.addReview(req).subscribe(
         res =>{
           this.dialogRef.close();
           this.snackbar.open("Review added.", "Close", {duration: 3000})
         },
         error => {
           this.snackbar.open(error.error, 'Close',{duration: 3000});
         }
       )
  }

  setReviewNumber(rating: number) {
    this.reviewForm.patchValue({reviewNumber : rating});
    console.log(this.reviewForm.value.reviewNumber);
  }
}
