import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CartService } from '../services/cart.service';

export const emptycartGuard: CanActivateFn = (route, state) => {
  const cartService = inject(CartService);
  const router = inject(Router);
  if (!cartService.cart() || cartService.cart()?.items.length === 0) {
    if (!state.url.endsWith('/cart')) router.navigateByUrl('/cart');
    return false;
  } else {
    return true;
  }
};
