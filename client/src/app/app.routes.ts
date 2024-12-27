import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { ProfileComponent } from './features/account/profile/profile.component';
import { authGuard } from './core/guards/auth.guard';
import { emptycartGuard } from './core/guards/emptycart.guard';
import { CheckoutSuccessComponent } from './features/checkout/checkout-success/checkout-success.component';
import { OrderComponent } from './features/orders/order.component';
import { OrderDetailComponent } from './features/orders/order-detail/order-detail.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'error', component: TestErrorComponent },
  { path: 'shop', component: ShopComponent },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent },
  {
    path: 'checkout',
    component: CheckoutComponent,
    canActivate: [authGuard, emptycartGuard],
  },
  {
    path: 'checkout/success',
    component: CheckoutSuccessComponent,
    canActivate: [authGuard, emptycartGuard],
  },
  { path: 'account/login', component: LoginComponent },
  { path: 'account/register', component: RegisterComponent },
  {
    path: 'account/orders',
    component: OrderComponent,
    canActivate: [authGuard],
  },
  {
    path: 'account/orders/:id',
    component: OrderDetailComponent,
    canActivate: [authGuard],
  },
  {
    path: 'account/profile',
    component: ProfileComponent,
    canActivate: [authGuard],
  },
  { path: 'error/not-found', component: NotFoundComponent },
  { path: 'error/server-error', component: ServerErrorComponent },
  { path: '**', redirectTo: 'error/not-found', pathMatch: 'full' },
];
