import { Component, inject, OnInit, output } from '@angular/core';
import { CheckoutService } from '../../../core/services/checkout.service';
import { MatRadioModule } from '@angular/material/radio';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../../core/services/cart.service';
import { DeliveryMethod } from '../../../shared/models/deliveryMethod';

@Component({
  selector: 'app-checkout-delivery',
  imports: [MatRadioModule, CurrencyPipe],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss',
})
export class CheckoutDeliveryComponent implements OnInit {
  checkoutSerivce = inject(CheckoutService);
  cartService = inject(CartService);
  deliveryComplete = output<boolean>();

  ngOnInit(): void {
    this.checkoutSerivce.getDeliveryMethods().subscribe({
      next: (methods) => {
        const deliveryMethodId = this.cartService.cart()?.deliveryMethodId;
        if (deliveryMethodId) {
          const method = methods.find((x) => x.id === deliveryMethodId);
          if (method) {
            this.cartService.selectedDelivery.set(method);
            this.deliveryComplete.emit(true);
          }
        }
      },
    });
  }

  updateDeliveryMethod(method: DeliveryMethod) {
    this.cartService.selectedDelivery.set(method);
    const cart = this.cartService.cart();
    if (cart) {
      cart.deliveryMethodId = method.id;
      this.cartService.setCart(cart);
      this.deliveryComplete.emit(true);
    }
  }
}
