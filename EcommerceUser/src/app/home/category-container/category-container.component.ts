import {Component, Input, OnInit} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterLink, RouterOutlet} from "@angular/router";
import {MatCard} from "@angular/material/card";
import {LoaderComponent} from "../../global/loader/loader.component";

@Component({
  selector: 'app-category-container',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, MatCard, LoaderComponent],
  templateUrl: './category-container.component.html',
  styleUrl: './category-container.component.css'
})
export class CategoryContainerComponent implements OnInit {
  @Input() category: any;

  ngOnInit(): void {
  }
}
