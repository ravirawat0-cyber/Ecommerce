import {Component, Output} from '@angular/core';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import EventEmitter from "node:events";
import {SidenavComponent} from "../sidenav/sidenav.component";
import {SidebarService} from "../sidenav/sidebar.service";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, MatIconModule, SidenavComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(private sidebarService: SidebarService) {
  }

  toggleSidebar(): void {
    this.sidebarService.toogleSidebar()
  }
}
