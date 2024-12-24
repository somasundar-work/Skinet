import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CartItemComponent } from './cart-item/cart-item.component';
import { EmptyCartComponent } from './empty-cart/empty-cart.component';
import { OrderSummaryComponent } from "../../shared/components/order-summary/order-summary.component";

@Component({
  selector: 'app-cart',
  imports: [
    MatIconModule,
    MatButtonModule,
    CartItemComponent,
    EmptyCartComponent,
    OrderSummaryComponent
],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent {
  cartService = inject(CartService);
}
