import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { switchMap } from 'rxjs';
import { environment } from '../../../environments/environment.development';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const http = inject(HttpClient);

  const apiUrl = environment.apiUrl + "/token/refresh";

  if (req.url.includes('token/refresh')) {
    return next(req);
  }

  if (typeof window !== 'undefined' && localStorage) {
    const token = localStorage.getItem('accessToken');
    const accessTokenExpiresAt = localStorage.getItem('accessTokenExpiresAt');
    const refreshToken = localStorage.getItem('refreshToken');

    if (accessTokenExpiresAt) {
      const expiresAt = new Date(accessTokenExpiresAt).getTime();
      const now = new Date().getTime();

      if (now > expiresAt) {

        if (refreshToken) {
          return http.post(apiUrl, null, { headers: req.headers.set("Refresh-Token" , refreshToken),
            observe: 'response'
          }).pipe(
            switchMap((response: any) => {
              const newAccessToken = response.headers.get('Access-Token');
              const newAccessTokenExpiresAt = response.headers.get('Access-Token-Expires-At');

              localStorage.setItem('accessToken', newAccessToken);
              localStorage.setItem('accessTokenExpiresAt', newAccessTokenExpiresAt);


              return next(req.clone({
                headers: req.headers.set("Access-Token", newAccessToken)
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
