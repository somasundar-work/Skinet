import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Cart, CartItem, Coupon } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import { firstValueFrom, map, tap } from 'rxjs';
import { DeliveryMethod } from '../../shared/models/deliveryMethod';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  private router = inject(Router);
  cart = signal<Cart | null>(null);
  itemCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => (sum += item.quantity), 0);
  });
  selectedDelivery = signal<DeliveryMethod | null>(null);
  selectedCurrency = signal<string>(environment.appCurrency);
  totals = computed(() => {
    const cart = this.cart();
    const delivery = this.selectedDelivery();
    if (!cart) return null;
    const subtotal = cart.items.reduce(
      (sum, item) => (sum += item.quantity * item.price),
      0
    );
    const shipping = delivery?.price || 0;

    let discountValue = 0;
    if (cart.coupon) {
      if (cart.coupon.amountOff) {
        discountValue = cart.coupon.amountOff;
      } else if (cart.coupon.percentOff) {
        discountValue = subtotal * (cart.coupon.percentOff / 100);
      }
    }

    return {
      subtotal,
      shipping,
      discount: discountValue,
      total: subtotal + shipping - discountValue,
    };
  });

  applyDiscount(code: string) {
    return this.http.get<Coupon>(this.baseUrl + 'coupon/' + code);
  }

  changeCurreny(currency: string) {
    this.selectedCurrency.set(currency);
    const cart = this.cart();
    if (cart != null) {
      this.deleteCart();
      this.router.navigateByUrl('/shop');
    }
  }

  getCart(id: string) {
    var params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<Cart>(this.baseUrl + 'Cart', { params }).pipe(
      map((cart) => {
        this.cart.set(cart);
        this.selectedCurrency.set(cart.currency);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + 'Cart', cart).pipe(
      tap((cart) => {
        this.cart.set(cart);
      })
    );
  }

  async addItemToCart(item: CartItem | Product, Quantity = 1) {
    const cart = this.cart() ?? this.createCart();
    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, Quantity);
    await firstValueFrom(this.setCart(cart));
  }

  async removeItemFromCart(productId: number, Quantity = 1) {
    const cart = this.cart();
    if (!cart) return;
    const index = cart.items.findIndex((x) => x.productId == productId);
    if (index !== -1) {
      if (cart.items[index].quantity > Quantity) {
        cart.items[index].quantity -= Quantity;
      } else {
        cart.items.splice(index, 1);
      }
      if (cart.items.length === 0) {
        this.deleteCart();
      } else {
        await firstValueFrom(this.setCart(cart));
      }
    }
  }

  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      },
    });
  }

  private addOrUpdateItem(
    items: CartItem[],
    item: CartItem,
    Quantity: number
  ): CartItem[] {
    const index = items.findIndex((x) => x.productId == item.productId);
    if (index === -1) {
      item.quantity = Quantity;
      items.push(item);
    } else {
      items[index].quantity += Quantity;
    }
    return items;
  }

  private mapProductToCartItem(item: Product): CartItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity: 0,
      brand: item.brand,
      category: item.category,
    };
  }

  private isProduct(item: CartItem | Product): item is Product {
    return (item as Product).id !== undefined;
  }

  private createCart(): Cart {
    const cart = new Cart();
    cart.currency = this.selectedCurrency();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}
