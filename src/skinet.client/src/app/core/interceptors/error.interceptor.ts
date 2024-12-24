import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackbar = inject(SnackbarService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      switch (err.status) {
        case 400:
          if (err.error.errors) {
            const modelStateErrors = [];
            for (const key in err.error.errors) {
              if (err.error.errors[key]) {
                modelStateErrors.push(err.error.errors[key]);
              }
            }
            throw modelStateErrors.flat();
          }
          snackbar.error(err.error.title || err.error);
          break;
        case 401:
          snackbar.error(err.error.title || err.error);
          break;
        case 404:
          router.navigateByUrl('error/not-found');
          break;
        case 500:
          const navigationExtras: NavigationExtras = {
            state: { error: err.error },
          };
          router.navigateByUrl('error/server-error', navigationExtras);
          break;
        default:
          break;
      }
      return throwError(() => err);
    })
  );
};
