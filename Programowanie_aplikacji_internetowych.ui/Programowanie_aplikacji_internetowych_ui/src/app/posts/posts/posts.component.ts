import { Component, OnInit } from '@angular/core';
import { GetPostsModel } from '../models/get-posts-model';
import { PostService } from '../services/post.service';

@Component({
  selector: 'app-posts',
  standalone: true,
  imports: [],
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.css'
})
export class PostsComponent implements OnInit{

  posts: GetPostsModel[] = [];

  ngOnInit(): void {
    this.postService.getPosts().subscribe(response => {
      console.log(response);
    })
  }

  constructor(private postService : PostService) {
    
  }

}
