import { Routes } from '@angular/router';
import { LoginComponent } from './auth/components/login/login.component';
import { PostsComponent } from './posts/posts/posts.component';
import { GetPostByIdComponent } from './posts/get-post-by-id/get-post-by-id.component';
import { loginGuard } from './auth/guards/login.guard';
import { authGuard } from './auth/guards/auth.guard';

export const routes: Routes = [
    {path: "", component: PostsComponent, canActivate: [authGuard]},
    { path: "login", component: LoginComponent, canActivate: [loginGuard] },
    { path: "posts", component: PostsComponent, canActivate: [authGuard] },
    {path: "post/:id", component: GetPostByIdComponent, canActivate: [authGuard]}
    
];
