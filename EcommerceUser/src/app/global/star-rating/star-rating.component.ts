import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {NgClass, NgForOf} from "@angular/common";


@Component({
  selector: 'app-star-rating',
  standalone: true,
  imports: [
    MatIcon,
    NgForOf,
    NgClass
  ],
  templateUrl: './star-rating.component.html',
  styleUrl: './star-rating.component.css'
})
export class StarRatingComponent implements OnInit {
  @Input() rating: number  = 1;
  @Input() starCount: number = 5;
  @Output() ratingChange = new EventEmitter<number>();

  stars: number[] = [];

  ngOnInit(): void {
    this.stars = Array(this.starCount).fill(0).map((_, i) => i + 1);
  }

  rate(star: number){
    this.rating = star;
    this.ratingChange.emit(this.rating);
  }


}
