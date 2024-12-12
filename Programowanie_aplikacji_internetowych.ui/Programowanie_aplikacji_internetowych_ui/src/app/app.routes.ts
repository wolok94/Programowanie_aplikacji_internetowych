import { Routes } from '@angular/router';
import { LoginComponent } from './auth/components/login/login.component';
import { PostsComponent } from './posts/posts/posts.component';

export const routes: Routes = [
    { path: "login", component: LoginComponent },
    {path: "posts", component: PostsComponent}
    
];
