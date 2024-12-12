import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetPostsModel } from '../models/get-posts-model';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  apiUrl : string = environment.apiUrl + "/Post"

  constructor(private httpClient: HttpClient) { }
  
  getPosts(): Observable<GetPostsModel[]> {
    return this.httpClient.get<GetPostsModel[]>(this.apiUrl + "/all");
  }
}
