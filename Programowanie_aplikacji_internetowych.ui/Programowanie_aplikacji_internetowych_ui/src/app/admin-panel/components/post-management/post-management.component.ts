import { Component, OnInit } from '@angular/core';
import { AdminModule } from '../../modules/admin/admin.module';
import { PostService } from '../../../posts/services/post.service';
import { GetPostsModel } from '../../../posts/models/get-posts-model';

@Component({
  selector: 'app-post-management',
  standalone: true,
  imports: [AdminModule],
  templateUrl: './post-management.component.html',
  styleUrl: './post-management.component.css'
})
export class PostManagementComponent implements OnInit {

  posts: GetPostsModel[] = [];

  constructor(private postService : PostService) {
    
  }
  ngOnInit(): void {
    this.postService.getPosts().subscribe(response => {
      this.posts = response;
    })
  }

  editPost(postId: string): void {
    console.log(`Edytowanie posta o ID: ${postId}`);
  }

  deletePost(postId: string): void {
    console.log(`Usuwanie posta o ID: ${postId}`);
  }
}
