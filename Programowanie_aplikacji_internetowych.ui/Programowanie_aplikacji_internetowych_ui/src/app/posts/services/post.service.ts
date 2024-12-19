import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetPostsModel } from '../models/get-posts-model';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs/internal/Observable';
import { GetPostByIdModel } from '../models/get-post-by-id-model';
import { CreateComment } from '../models/create-comment';
import { CreatePost } from '../models/create-post-model';
import { UpdatePost } from '../models/update-post-model';

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

  createPost(post : CreatePost):Observable<void> {
    return this.httpClient.post<void>(this.apiUrl + "/createPost", post);
  }

  createPostFromCsv(file: FormData):Observable<void> {
    return this.httpClient.post<void>(this.apiUrl + "/createFromCsv", file);
  }

  deletePost(postId: string): Observable<void>{
    return this.httpClient.delete<void>(this.apiUrl + "/delete/" + postId);
  }

  updatePost(postId: string, post: UpdatePost): Observable<void>{
    return this.httpClient.patch<void>(this.apiUrl + "/update/" + postId, post);
  }
}
