import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  Rocket,
  Menu,
  LucideAngularModule
} from 'lucide-angular';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  isMenuOpen = false;
  rocket = Rocket;
  menu = Menu

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
