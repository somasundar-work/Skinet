import { DatePipe, CurrencyPipe, CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { OrderService } from '../../../core/services/order.service';
import { Order, ShippingAddress } from '../../../shared/models/order';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-order-detail',
  imports: [
    MatCardModule,
    MatButton,
    DatePipe,
    CurrencyPipe,
    CommonModule,
    RouterLink,
  ],
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss'],
})
export class OrderDetailComponent implements OnInit {
  private orderService = inject(OrderService);
  private activatedRoute = inject(ActivatedRoute);
  order?: Order;

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.orderService.getOrderDetails(+id).subscribe({
      next: (order) => (this.order = order),
    });
  }

  getFormattedAddress(address: ShippingAddress): string {
    return `${address.name}, ${address.line1}, ${
      address.line2 ? address.line2 + ', ' : ''
    }${address.city}, ${address.state}, ${address.postalCode}, ${
      address.country
    }`;
  }
}
