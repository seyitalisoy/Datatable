import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LinkComponent } from './components/link.component/link.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,LinkComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('frontend');
}
