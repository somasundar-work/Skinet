import {
  ApplicationConfig,
  provideZoneChangeDetection,
  provideAppInitializer,
  inject,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { InitService } from './core/services/init.service';
import { lastValueFrom } from 'rxjs';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor])),
    provideAppInitializer(initializeApp),
  ],
};

/**
 * Initializes the application by invoking the InitService.
 *
 * This function is used as an application initializer to perform necessary
 * startup tasks before the Angular app is fully initialized. It waits for the
 * InitService to complete its initialization process and then removes the
 * initial splash screen from the DOM.
 *
 * @returns A promise that resolves when the initialization is complete.
 *
 * @see {@link https://angular.dev/api/core/provideAppInitializer?tab=usage-notes}
 */
function initializeApp() {
  const initService = inject(InitService);
  return lastValueFrom(initService.init()).finally(() => {
    const splash = document.getElementById('initial-splash');
    if (splash) {
      splash.remove();
    }
  });
}
