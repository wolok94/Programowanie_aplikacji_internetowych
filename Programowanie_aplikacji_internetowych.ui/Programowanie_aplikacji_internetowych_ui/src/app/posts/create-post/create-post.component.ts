import { Component } from '@angular/core';
import { PostModule } from '../modules/post/post.module';
import { QuillModule } from 'ngx-quill';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../services/post.service';
import { CreatePost } from '../models/create-post-model';
import { MessageService } from '../../shared/services/message.service';

@Component({
  selector: 'app-create-post',
  standalone: true,
  imports: [PostModule,  QuillModule, ReactiveFormsModule],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.css'
})
export class CreatePostComponent {

  postForm: FormGroup;
  submittedPost: { title: string; text: string; imageUrl: string } | null = null;

  constructor(private fb: FormBuilder, private postService : PostService, private messageService : MessageService) {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      text: ['', Validators.required],
      imageUrl: ['', Validators.pattern(/^(https?:\/\/.+|data:image\/(png|jpeg|gif|bmp);base64,.+)$/)],
    });
  }

  onSubmit(): void {
    if (this.postForm.valid) {
      let post: CreatePost = this.postForm.value;
      this.postService.createPost(post).subscribe(response => {
        this.messageService.showMessage("Pomyślnie dodano post", "success");
      })
      this.postForm.reset();
    }
  }
}
