import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import * as jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl: string = environment.apiUrl + "/User";
  isLogged: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.checkStoredLogin());
  logged: boolean = false;
  isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private httpClient: HttpClient, ) { 

  }
  
  login(loginModel: LoginModel): Observable<HttpResponse<any>> {
    return this.httpClient.post<HttpResponse<any>>(this.apiUrl + "/Login", loginModel, { observe: 'response' });
  }

  saveTokens(accessToken: string, refreshToken: string, accessTokenExpiresAt: string, refreshTokenExpiresAt: string) {
    console.log("dodaje " + accessToken);
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('accessTokenExpiresAt', accessTokenExpiresAt.toString());
    localStorage.setItem('refreshTokenExpiresAt', refreshTokenExpiresAt.toString());
    localStorage.setItem('isLoggedIn', 'true');
    this.isLogged.next(true);
  }

  removeTokens(){
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('accessTokenExpiresAt');
    localStorage.removeItem('refreshTokenExpiresAt');
    localStorage.removeItem('isLoggedIn');
    this.isLogged.next(false);
  }

  checkRole(accessToken : string) {
    const decodedToken = accessToken ? jwt_decode.jwtDecode(accessToken) : null;
    let decodedTokenJson = JSON.parse(JSON.stringify(decodedToken));
    let role = decodedTokenJson["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    return role === "Admin";

  }

  private checkStoredLogin(): boolean {
    return localStorage.getItem('isLoggedIn') === 'true';
  }

}
