import { Component, inject, OnDestroy } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { SignalrService } from '../../../core/services/signalr.service';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-checkout-success',
  imports: [
    MatButtonModule,
    RouterLink,
    MatIconModule,
    CurrencyPipe,
    DatePipe,
    NgIf,
    MatProgressSpinnerModule,
  ],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss',
})
export class CheckoutSuccessComponent implements OnDestroy {
  signalrService = inject(SignalrService);

  ngOnDestroy(): void {
    this.signalrService.orderSignal.set(null);
  }
}
