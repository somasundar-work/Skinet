import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-empty-cart',
  imports: [MatIconModule, MatButtonModule, RouterLink],
  templateUrl: './empty-cart.component.html',
  styleUrl: './empty-cart.component.scss',
})
export class EmptyCartComponent {
  cartService = inject(CartService);
}
