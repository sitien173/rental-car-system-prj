import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {AppModule} from './app/app.module';
import {loadCldr, registerLicense, setCulture, setCurrencyCode, validateLicense} from '@syncfusion/ej2-base';
import {enableProdMode, LOCALE_ID} from "@angular/core";
import * as viNumberData from 'cldr-data/main/vi/numbers.json';
import * as viTimeZoneData from 'cldr-data/main/vi/timeZoneNames.json';
import * as viCaGregorianData from 'cldr-data/main/vi/ca-gregorian.json';
import * as viNumbers from 'cldr-data/supplemental/numberingSystems.json';
import * as viCurrencies from 'cldr-data/main/vi/currencies.json';
import {environment} from "@ptit.rentalcar.app-config";

// Registering Syncfusion license key
registerLicense(environment.syncfusionLicense);
validateLicense();

if (environment.production) {
  enableProdMode();
}

setCulture('vi');
setCurrencyCode('VND');

loadCldr(viNumberData, viTimeZoneData, viCaGregorianData, viNumbers, viCurrencies);

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
