import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatDivider} from "@angular/material/divider";
import {MatIcon} from "@angular/material/icon";
import {MatListItem} from "@angular/material/list";
import {MatSidenav, MatSidenavContainer, MatSidenavContent} from "@angular/material/sidenav";
import {HomeContainerComponent} from "../../home/home-container/home-container.component";
import {RouterLink, RouterOutlet} from "@angular/router";
import {NgClass, NgIf} from "@angular/common";
import {SidebarService} from "./sidebar.service";
import {AccountService} from "../../services/account.service";
import {Subscription} from "rxjs";
import {IUserRes} from "../../models/user.model";

@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [
    MatButton,
    MatDivider,
    MatIcon,
    MatIconButton,
    MatListItem,
    MatSidenav,
    MatSidenavContainer,
    MatSidenavContent,
    RouterOutlet,
    NgClass,
    NgIf,
    RouterLink,
  ],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent implements OnInit{
  isOpen: boolean = false;
  userSubcription ! : Subscription;
  userDetail : IUserRes | null = null;
  userName : string | undefined = ""

 constructor(private sidebarService: SidebarService, private accountServices : AccountService,) {
   this.sidebarService.isOpen$.subscribe(isOpen => {
     this.isOpen = isOpen;
   })
 }

  ngOnInit(): void {
       this.userSubcription = this.accountServices.user$.subscribe(user => {
            this.userDetail = user;
            this.userName = user?.user.name;
       })
    }

    ngOnDestroy() {
     this.userSubcription.unsubscribe();
    }

  closeSideNavbar() {
    this.isOpen = false;
  }

  userlogout() {
    this.accountServices.logout();
  }

}
