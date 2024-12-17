import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { GetPostByIdModel } from '../models/get-post-by-id-model';
import { ActivatedRoute } from '@angular/router';
import { PostModule } from '../modules/post/post.module';
import { CreateComment } from '../models/create-comment';

@Component({
  selector: 'app-get-post-by-id',
  standalone: true,
  imports: [PostModule],
  templateUrl: './get-post-by-id.component.html',
  styleUrl: './get-post-by-id.component.css'
})
export class GetPostByIdComponent implements OnInit {

  post!: GetPostByIdModel;
  newComment: CreateComment = {
    postId: "",
    text: ""
};
  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.postService.getPostById(id).subscribe(response => {
        this.post = response;
        console.log(this.post);
        console.log(response);
      })
    }

  }

  constructor(private postService : PostService, private route: ActivatedRoute) {
    
  }

  addComment() {
    if (this.newComment.text.trim() === '') {
      throw new Error("Komentarz nie może być pusty.");
    }
      this.newComment.postId = this.post.id;
      this.postService.addCommentToPost(this.newComment).subscribe(response => {
      console.log(response);
      });
  }


}
