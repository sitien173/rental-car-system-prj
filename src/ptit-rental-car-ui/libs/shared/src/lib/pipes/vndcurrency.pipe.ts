import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'vndCurrency'
})
export class VNDCurrencyPipe implements PipeTransform {

  transform(value: number | undefined): string {
    if (value == undefined)
      return '';

    // Check if the value is not a number
    if (isNaN(value)) {
      return '';
    }

    // Convert the value to VND currency format
    const formatter = new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
      minimumFractionDigits: 0
    });

    return formatter.format(value);
  }

}
