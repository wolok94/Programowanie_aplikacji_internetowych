import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetPostsModel } from '../models/get-posts-model';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs/internal/Observable';
import { GetPostByIdModel } from '../models/get-post-by-id-model';
import { CreateComment } from '../models/create-comment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  apiUrl : string = environment.apiUrl + "/Post"

  constructor(private httpClient: HttpClient) { }
  
  getPosts(): Observable<GetPostsModel[]> {
    return this.httpClient.get<GetPostsModel[]>(this.apiUrl + "/all");
  }

  getPostById(id: string): Observable<GetPostByIdModel>{
    return this.httpClient.get<GetPostByIdModel>(this.apiUrl + "/" + id);
  }

  addCommentToPost(comment : CreateComment): Observable<void>{
    return this.httpClient.post<void>(environment.apiUrl + "/comment/createComment", comment);
  }
}
