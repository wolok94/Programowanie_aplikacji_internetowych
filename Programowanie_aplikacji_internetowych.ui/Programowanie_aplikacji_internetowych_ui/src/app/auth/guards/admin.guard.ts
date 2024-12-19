import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService)
  const token = localStorage.getItem("accessToken");
  const router = inject(Router)

  if (token) {
    let isAdmin = authService.checkRole(token);

    if (isAdmin) {
      return true;
    }
  }
  router.navigate(['/posts']);
  return false;
};
