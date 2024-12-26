import { nanoid } from 'nanoid';
import { environment } from '../../../environments/environment';
export type CartType = {
  id: string;
  items: CartItem[];
  deliveryMethodId?: number;
  clientSecret?: string;
  paymentIntentId?: string;
  currency: string;
};

export type CartItem = {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  category: string;
};

export class Cart implements CartType {
  id: string = nanoid();
  items: CartItem[] = [];
  deliveryMethodId?: number;
  clientSecret?: string;
  paymentIntentId?: string;
  currency: string = environment.appCurrency;
}
