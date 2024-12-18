import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const loginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem("accessToken");

  if (token) {
    router.navigate(["/posts"]);
    return false;
  } else {
    return true;    
  }

};
