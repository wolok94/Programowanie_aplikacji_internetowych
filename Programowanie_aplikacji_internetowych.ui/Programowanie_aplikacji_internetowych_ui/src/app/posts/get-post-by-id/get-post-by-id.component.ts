import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { GetPostByIdModel } from '../models/get-post-by-id-model';
import { ActivatedRoute } from '@angular/router';
import { PostModule } from '../modules/post/post.module';

@Component({
  selector: 'app-get-post-by-id',
  standalone: true,
  imports: [PostModule],
  templateUrl: './get-post-by-id.component.html',
  styleUrl: './get-post-by-id.component.css'
})
export class GetPostByIdComponent implements OnInit {

  post! : GetPostByIdModel;
  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.postService.getPostById(id).subscribe(response => {
        this.post = response;
      })
    }

  }

  constructor(private postService : PostService, private route: ActivatedRoute) {
    
  }


}
