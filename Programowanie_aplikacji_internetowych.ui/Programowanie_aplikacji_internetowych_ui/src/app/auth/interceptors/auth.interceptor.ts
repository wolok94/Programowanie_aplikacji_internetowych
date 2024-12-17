import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { switchMap } from 'rxjs';
import { environment } from '../../../environments/environment.development';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const http = inject(HttpClient);

  const apiUrl = environment.apiUrl + "/User/RefreshToken";

  if (req.url.includes('/User/RefreshToken')) {
    return next(req);
  }

  if (typeof window !== 'undefined' && localStorage) {
    let token = localStorage.getItem('accessToken');
    const accessTokenExpiresAt = localStorage.getItem('accessTokenExpiresAt');
    const refreshToken = localStorage.getItem('refreshToken');

    if (accessTokenExpiresAt) {
      const [dateSplit, timeSplit] = accessTokenExpiresAt.split(" ");
      const [hours, minutes, seconds] = timeSplit.split(":");
      const [day, month, year] = dateSplit.split(".");
      const isoString = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;

      const timestamp = new Date(isoString).getTime();
      const expiresAt = new Date(timestamp).getTime();
      const now = new Date().getTime();

      if (now > expiresAt) {

        if (refreshToken && token) {
          return http.post(apiUrl, null, {
            headers: req.headers.set("Refresh-Token", refreshToken)
              .set("Access-Token", token),
            observe: 'response'
          }).pipe(
            switchMap((response: any) => {
              const newAccessToken = response.headers.get("x-access-token");
              const newRefreshToken = response.headers.get("x-refresh-token");
              const newAccessTokenExpiresAt = response.headers.get("x-access-token-expiresat");
              const newRefreshTokenExpiresAt = response.headers.get("x-refresh-token-expiresat");

              localStorage.setItem('accessToken', newAccessToken);
              localStorage.setItem('refreshToken', newRefreshToken);
              localStorage.setItem('accessTokenExpiresAt', newAccessTokenExpiresAt.toString());
              localStorage.setItem('refreshTokenExpiresAt', newRefreshTokenExpiresAt.toString());
              token = newAccessToken;

              return next(req.clone({
                headers: req.headers.set("Authorization", "Bearer " + newAccessToken)
              }));
            })
          );
        }
      }
    }


    if (token) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization", "Bearer " + token)
      });
      return next(cloned);
    }
  }

  return next(req);

};
