import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl: string = environment.apiUrl + "/User";
  isLogged: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private httpClient: HttpClient) { }
  
  login(loginModel: LoginModel): Observable<HttpResponse<any>> {
    return this.httpClient.post<HttpResponse<any>>(this.apiUrl + "/Login", loginModel, { observe: 'response' });
  }

  saveTokens(accessToken: string, refreshToken: string, accessTokenExpiresAt: string, refreshTokenExpiresAt: string) {
    console.log("dodaje " + accessToken);
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
