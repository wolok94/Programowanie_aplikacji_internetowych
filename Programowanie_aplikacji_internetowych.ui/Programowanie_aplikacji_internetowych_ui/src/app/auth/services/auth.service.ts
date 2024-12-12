import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl : string = environment.apiUrl + "/User"

  constructor(private httpClient: HttpClient) { }
  
  login(loginModel: LoginModel): Observable<HttpResponse<void>> {
    return this.httpClient.post<HttpResponse<void>>(this.apiUrl + "/Login", loginModel);
  }

  saveTokens(accessToken: string, refreshToken: string, accessTokenExpiresAt: string, refreshTokenExpiresAt: string){
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('accessTokenExpiresAt', accessTokenExpiresAt.toString());
    localStorage.setItem('refreshTokenExpiresAt', refreshTokenExpiresAt.toString());
  }

  removeTokens(){
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessTokenExpiresAt');
    localStorage.removeItem('refreshTokenExpiresAt');


  }

}
