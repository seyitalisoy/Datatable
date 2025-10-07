import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LinkComponent } from './components/link.component/link.component';
import { NavbarComponent } from './components/navbar.component/navbar.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,LinkComponent,NavbarComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');
}
