export type Order = {
  id: number;
  orderDate: Date;
  buyerEmail: string;
  shippingAddress: ShippingAddress;
  deliveryMethod: string;
  shippingPrice: number;
  paymentSummary: PaymentSummary;
  orderItems: OrderItem[];
  subtotal: number;
  status: string;
  total: number;
  paymentIntentId: string;
};

export type ShippingAddress = {
  name: string;
  line1: string;
  line2?: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
};

export type PaymentSummary = {
  last4: number;
  brand: string;
  expMonth: number;
  expYear: number;
};

export type OrderItem = {
  productId: number;
  name: string;
  description: string;
  pictureUrl?: string;
  brand: string;
  category: string;
  price: number;
  quantity: number;
};

export type OrderToCreate = {
  cartId: string;
  deliveryMethodId: number;
  shippingAddress: ShippingAddress;
  paymentSummary: PaymentSummary;
};
