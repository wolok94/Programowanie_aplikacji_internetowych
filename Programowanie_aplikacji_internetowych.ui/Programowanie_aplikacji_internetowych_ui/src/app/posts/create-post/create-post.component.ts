import { Component } from '@angular/core';
import { PostModule } from '../modules/post/post.module';
import { QuillModule } from 'ngx-quill';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../services/post.service';
import { CreatePost } from '../models/create-post-model';
import { MessageService } from '../../shared/services/message.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { GetPostByIdModel } from '../models/get-post-by-id-model';

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [PostModule,  QuillModule, ReactiveFormsModule],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.css'
})
export class CreatePostComponent {

  post!: GetPostByIdModel;
  postForm: FormGroup;
  submittedPost: { title: string; text: string; imageUrl: string } | null = null;
  isEditMode: boolean = false;
  postId!: string;

  constructor(private fb: FormBuilder, private postService: PostService, private messageService: MessageService, private activatedRoute: ActivatedRoute
    , private route : Router) {
    let id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.postId = id;
      this.postService.getPostById(id).subscribe(response => {
        this.post = response;
        this.isEditMode = this.post !== null;
        
        this.postForm.patchValue({
          title: this.post.title,
          text: this.post.text,
          imageUrl: this.post.imageUrl,
        });
      })
    }
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      text: ['', Validators.required],
      imageUrl: ['', Validators.pattern(/^(https?:\/\/.+|data:image\/(png|jpeg|gif|bmp);base64,.+)$/)],
    });

  }

  onSubmit(): void {
    if (this.postForm.valid) {
      let post: CreatePost = this.postForm.value;
      if (this.post) {
        this.postService.updatePost(this.postId, post).subscribe({
          next: () => {
            this.messageService.showMessage("Pomyślnie zaktualizowano post", "success");
          }, error: (err) => {
            this.messageService.showMessage("Wystąpił błąd podczas aktualizowania postu. Spróbuj ponownie.", "error");
          }
        })
      } else {
        this.postService.createPost(post).subscribe({
          next: () => {
            this.messageService.showMessage("Pomyślnie dodano post", "success");
          }, error: (err) => {
            this.messageService.showMessage("Wystąpił błąd podczas dodawania postu. Spróbuj ponownie.", "error");
          }

        })
      }

      this.postForm.reset();
    }
  }

  navigateToCsv() {
    this.route.navigate(["/createPostFromCsv"])
  }
}
