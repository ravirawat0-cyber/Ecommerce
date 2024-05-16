import {Component, Inject, OnInit} from '@angular/core';
import {MatButton, MatIconButton} from "@angular/material/button";
import {MAT_DIALOG_DATA, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatIcon} from "@angular/material/icon";
import {MatInput} from "@angular/material/input";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {StarRatingComponent} from "../../global/star-rating/star-rating.component";
import {ReviewService} from "../../services/review.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {IReviewReq} from "../../models/review.model";

@Component({
  selector: 'app-review-update-model',
  standalone: true,
  imports: [
    MatButton,
    MatDialogContent,
    MatFormField,
    MatIcon,
    MatIconButton,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    StarRatingComponent
  ],
  templateUrl: './review-update-model.component.html',
  styleUrl: './review-update-model.component.css'
})
export class ReviewUpdateModelComponent implements OnInit{
  reviewForm ! : FormGroup;
  productId! : number;

  constructor(public dialogRef : MatDialogRef<ReviewUpdateModelComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private fb: FormBuilder,
              private reviewService: ReviewService,
              private snackBar : MatSnackBar,
              ) {
    this.productId = data.productId;
  }

  ngOnInit(): void {
    this.reviewForm = this.fb.group({
      reviewNumber : [1, Validators.required],
      review : ["", Validators.required],
    })
  }


  setReviewNumber(rating: number) {
    this.reviewForm.patchValue({reviewNumber : rating});
    console.log("update" , this.reviewForm.value.reviewNumber);
  }

  updateReview() {
    console.log("prod",this.productId);
    const req : IReviewReq = {
      rating : this.reviewForm.value.reviewNumber,
      comments : this.reviewForm.value.review,
      productId : this.productId,
    }

    this.reviewService.UpdateReview(req).subscribe(
      res => {
        this.dialogRef.close();
        this.snackBar.open("Review updated successfully.", "Close", {duration: 3000})
    },
    error => {
      this.snackBar.open("Something went wrong", 'Close', {duration: 3000})
    }
    )
  }
}
