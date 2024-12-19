import { Component, OnInit } from '@angular/core';
import { GetPostsModel } from '../models/get-posts-model';
import { PostService } from '../services/post.service';
import { Router } from '@angular/router';
import { PostModule } from '../modules/post/post.module';

@Component({
  selector: 'app-posts',
  standalone: true,
  imports: [PostModule],
  templateUrl: './posts.component.html',
  styleUrl: './posts.component.css'
})
export class PostsComponent implements OnInit{

  posts: GetPostsModel[] = [];

  ngOnInit(): void {
    this.postService.getPosts().subscribe(response => {
      this.posts = response;

    })
  }

  constructor(private postService : PostService, private router: Router) {
    
  }

  showDetails(id: string) {
    this.router.navigate(["post/" + id]);
  }

}
