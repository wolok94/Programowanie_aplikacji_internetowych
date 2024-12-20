import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { GetUsersModel } from '../models/get-users-model';
import { RoleModel } from '../models/role-model';
import { Observable } from 'rxjs';
import { ChangeRole } from '../models/change-role-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl: string = environment.apiUrl + "/Users";

  constructor(private httpClient: HttpClient) { }
  
  getUsers(): Observable<GetUsersModel[]>{
    return this.httpClient.get<GetUsersModel[]>(this.apiUrl);
  }

  getRoles(): Observable<RoleModel[]>{
    return this.httpClient.get<RoleModel[]>(environment.apiUrl + "/roles");
  }

  updateUser(changeRole : ChangeRole): Observable<void>{
    return this.httpClient.patch<void>(this.apiUrl + "/changeRole", changeRole);
  }
}
