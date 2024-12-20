import { Component, OnInit } from '@angular/core';
import { AdminModule } from '../../modules/admin/admin.module';
import { PostService } from '../../../posts/services/post.service';
import { GetPostsModel } from '../../../posts/models/get-posts-model';
import { Router } from '@angular/router';
import { MessageService } from '../../../shared/services/message.service';

@Component({
  selector: 'app-post-management',
  standalone: true,
  imports: [AdminModule],
  templateUrl: './post-management.component.html',
  styleUrl: './post-management.component.css'
})
export class PostManagementComponent implements OnInit {

  posts: GetPostsModel[] = [];

  constructor(private postService : PostService, private router: Router, private messageService: MessageService) {
    
  }
  ngOnInit(): void {
    this.getPosts();
  }

  getPosts() {
    this.postService.getPosts().subscribe(response => {
      this.posts = response;
    })
  }

  editPost(postId: string): void {
    this.router.navigate(['updatePost/', postId]);
  }

  deletePost(postId: string): void {
    this.postService.deletePost(postId).subscribe({
      next: () => {
        this.getPosts();
        this.messageService.showMessage("Pomyślnie usunięto post", "success");
      },
      error: (err) => {
        this.messageService.showMessage("Wystąpił błąd podczas usuwania postu. Spróbuj ponownie.", "error");
      }
    });
  }
  
}
