import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ConfirmationToken } from '@stripe/stripe-js';

@Pipe({
  name: 'payment',
})
export class PaymentPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(
    value: ConfirmationToken['payment_method_preview'] | undefined,
    ...args: unknown[]
  ): SafeHtml | undefined {
    if (value?.card) {
      const { brand, funding, last4, exp_month, exp_year } = value.card;
      const paymentHtml = `
        <div class="p-4 border rounded shadow-md w-full h-full">
          <h3 class="text-xl font-bold mb-2 border-b border-black text-right">Payment Details</h3>
          <p class="text-lg font-semibold">Card Brand: ${brand}</p>
          <p class="mt-2">Cart Type: ${funding}</p>
          <p class="mt-2">Last 4 Digits: **** **** **** ${last4}</p>
          <p class="mt-2">Expiry: ${exp_month}/${exp_year}</p>
        </div>
      `;
      return this.sanitizer.bypassSecurityTrustHtml(paymentHtml);
    } else {
      return undefined;
    }
  }
}
