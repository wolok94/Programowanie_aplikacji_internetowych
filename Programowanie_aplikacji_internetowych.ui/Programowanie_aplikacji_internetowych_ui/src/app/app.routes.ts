import { Routes } from '@angular/router';
import { LoginComponent } from './auth/components/login/login.component';
import { PostsComponent } from './posts/posts/posts.component';
import { GetPostByIdComponent } from './posts/get-post-by-id/get-post-by-id.component';

export const routes: Routes = [
    { path: "login", component: LoginComponent },
    { path: "posts", component: PostsComponent },
    {path: "post/:id", component: GetPostByIdComponent}
    
];
