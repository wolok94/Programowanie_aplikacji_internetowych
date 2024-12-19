import { Component } from '@angular/core';
import { PostModule } from '../modules/post/post.module';
import { QuillModule } from 'ngx-quill';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostService } from '../services/post.service';
import { CreatePost } from '../models/create-post-model';
import { MessageService } from '../../shared/services/message.service';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private fb: FormBuilder, private postService: PostService, private messageService: MessageService, private route : ActivatedRoute) {
    let id = this.route.snapshot.paramMap.get('id');

    if (id) {
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
      this.postService.createPost(post).subscribe(response => {
        this.messageService.showMessage("Pomyślnie dodano post", "success");
      })
      this.postForm.reset();
    }
  }
}
