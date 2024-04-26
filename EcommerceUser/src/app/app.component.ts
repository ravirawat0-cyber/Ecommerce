import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import {HeaderComponent} from "./global/header/header.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet , MatSlideToggleModule, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EcommerceUser';
}
