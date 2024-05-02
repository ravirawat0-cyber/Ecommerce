import {Component, ViewChild} from '@angular/core';
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatDivider} from "@angular/material/divider";
import {MatIcon} from "@angular/material/icon";
import {MatListItem} from "@angular/material/list";
import {MatSidenav, MatSidenavContainer, MatSidenavContent} from "@angular/material/sidenav";
import {HomeContainerComponent} from "../../home/home-container/home-container.component";
import {RouterOutlet} from "@angular/router";
import {NgClass, NgIf} from "@angular/common";
import {SidebarService} from "./sidebar.service";

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
  ],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent {
  isOpen: boolean = false;

 constructor(private sidebarService: SidebarService) {
   this.sidebarService.isOpen$.subscribe(isOpen => {
     this.isOpen = isOpen;
   })
 }

  closeSideNavbar() {
    this.isOpen = false;
  }
}
