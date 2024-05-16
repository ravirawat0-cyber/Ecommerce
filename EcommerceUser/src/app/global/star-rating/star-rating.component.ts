import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {NgClass, NgForOf, NgStyle} from "@angular/common";


@Component({
  selector: 'app-star-rating',
  standalone: true,
  imports: [
    MatIcon,
    NgForOf,
    NgClass,
    NgStyle
  ],
  templateUrl: './star-rating.component.html',
  styleUrl: './star-rating.component.css'
})
export class StarRatingComponent implements OnInit {
  @Input() rating: number  = 1;
  @Input() starCount: number = 5;
  @Input() readOnly: boolean = false;// Input for custom styles
  @Input() parentStyle!: any;
  @Input() childStyle!: any;
  @Input() buttonStyle! : any;
  @Output() ratingChange = new EventEmitter<number>();

  stars: number[] = [];

  ngOnInit(): void {
    this.stars = Array(this.starCount).fill(0).map((_, i) => i + 1);
  }

  rate(star: number){
   if(!this.readOnly) {
     this.rating = star;
     this.ratingChange.emit(this.rating);
   }
  }


}
