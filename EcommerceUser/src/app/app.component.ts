import {Component, OnInit, ViewChild} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import {HeaderComponent} from "./global/header/header.component";
import {HomeContainerComponent} from "./home/home-container/home-container.component";
import {SidenavComponent} from "./global/sidenav/sidenav.component";
import {MatSidenavContainer} from "@angular/material/sidenav";
import {AccountService} from "./services/account.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSlideToggleModule, HeaderComponent, HomeContainerComponent, SidenavComponent, MatSidenavContainer,],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  title = 'EcommerceUser';
  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.accountService.loadUserFromToken().subscribe();
  }

}
