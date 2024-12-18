import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem("accessToken");
  const refreshTokenExpiresAt = localStorage.getItem("refreshTokenExpiresAt");
  const currentDate = new Date().getTime();

  
  if (!token) {
    if (refreshTokenExpiresAt) {
      const refreshTokenExpiryDate = new Date(refreshTokenExpiresAt).getTime();
      
      
      if (refreshTokenExpiryDate < currentDate) {
        router.navigate(['/login']); 
        return false;
      }
    } else {
      router.navigate(['/login']); 
      return false;
    }
  }


  return true;

};
