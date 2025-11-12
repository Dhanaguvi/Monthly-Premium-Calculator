import { Component } from '@angular/core';
import { PremiumComponent } from './features/premium/premium.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, PremiumComponent],
  templateUrl: './app.component.html'
})
export class AppComponent {}

