import { HttpClient } from '@angular/common/http';
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
  
  login(loginModel: LoginModel): Observable<void> {
    console.log(this.apiUrl);
    return this.httpClient.post<void>(this.apiUrl + "/Login", loginModel);
  }
}
