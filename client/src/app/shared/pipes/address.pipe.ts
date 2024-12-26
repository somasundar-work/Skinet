import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ConfirmationToken } from '@stripe/stripe-js';

@Pipe({
  name: 'address',
})
export class AddressPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(
    value: ConfirmationToken['shipping'] | undefined,
    ...args: unknown[]
  ): SafeHtml | undefined {
    if (value?.address && value.name) {
      const { line1, line2, city, state, country, postal_code } = value.address;
      const addressHtml = `
        <div class="p-4 border rounded shadow-md w-full h-full">
          <h3 class="text-xl font-bold mb-2 border-b border-black text-right">Shipping Address</h3>
          <p class="text-lg font-semibold">Name: ${value.name}</p>
          <p class="mt-2">
            ${line1}${line2 ? `<br>${line2}` : ''}
            <br>${city}, ${state}
            <br>${postal_code}
            <br>${country}
          </p>
        </div>
      `;
      return this.sanitizer.bypassSecurityTrustHtml(addressHtml);
    } else {
      return undefined;
    }
  }
}
