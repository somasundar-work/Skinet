import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  itemCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => (sum += item.quantity), 0);
  });
  totals = computed(() => {
    const cart = this.cart();
    if (!cart)
      return {
        subtotal: 0,
        shipping: 0,
        discount: 0,
        total: 0,
      };
    const subtotal = cart.items.reduce(
      (sum, item) => (sum += item.quantity * item.price),
      0
    );
    const shipping = 0;
    const discount = 0;

    return {
      subtotal,
      shipping,
      discount,
      total: subtotal + shipping - discount,
    };
  });

  getCart(id: string) {
    var params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<Cart>(this.baseUrl + 'Cart', { params }).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + 'Cart', cart).subscribe({
      next: (cart) => this.cart.set(cart),
    });
  }

  addItemToCart(item: CartItem | Product, Quantity = 1) {
    const cart = this.cart() ?? this.createCart();
    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, Quantity);
    this.setCart(cart);
  }

  removeItemFromCart(productId: number, Quantity = 1) {
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
        this.deleteCart(cart.id);
      } else {
        this.setCart(cart);
      }
    }
  }

  deleteCart(id: string) {
    var params = new HttpParams();
    params = params.append('id', id);
    this.http.delete(this.baseUrl + 'Cart', { params }).subscribe({
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
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}
