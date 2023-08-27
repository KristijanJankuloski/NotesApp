import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ApiRequestService } from './api-request.service';

export const authGuard: CanActivateFn = (route, state) => {
  const api = inject(ApiRequestService);
  const router = inject(Router);
  if(api.isLoggedIn)
    return true;
  router.navigate(['/login']);
  return false;
};
