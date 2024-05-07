import {Component, OnDestroy, OnInit, Output} from '@angular/core';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import EventEmitter from "node:events";
import {SidenavComponent} from "../sidenav/sidenav.component";
import {SidebarService} from "../sidenav/sidebar.service";
import {MatBadge} from "@angular/material/badge";
import {RouterLink, RouterOutlet} from "@angular/router";
import {AccountService} from "../../services/account.service";
import {Subscription} from "rxjs";
import {totalmem} from "node:os";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, MatIconModule, SidenavComponent, MatBadge, RouterLink, RouterOutlet],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit, OnDestroy {

  userSubscription ! : Subscription;
  TotalCartItem : number | undefined;
  constructor(private sidebarService: SidebarService, private accountService: AccountService) {
  }


  ngOnInit(): void {
        this.userSubscription = this.accountService.user$.subscribe(user =>{
          this.TotalCartItem = user?.cart.totalItems
        })
    }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  toggleSidebar(): void {
    this.sidebarService.toogleSidebar()
  }

  protected readonly totalmem = totalmem;
}
