import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginModel } from '../../models/login-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(private authService : AuthService, private fb: FormBuilder, private router : Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]], 

    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      let login: LoginModel = this.loginForm.value;
      this.authService.login(login).subscribe(response => {
        const accessToken = response.headers.get("x-access-token");
        const refreshToken = response.headers.get("x-refresh-token");
        const accessTokenExpiresAt = response.headers.get("x-access-token-expiresat");
        const refreshTokenExpiresAt = response.headers.get("x-refresh-token-expiresat");

        if(accessToken && refreshToken && accessTokenExpiresAt && refreshTokenExpiresAt){
          this.authService.saveTokens(accessToken, refreshToken, accessTokenExpiresAt, refreshTokenExpiresAt);
          this.authService.isLogged.next(true);
        }

        localStorage.setItem('isLoggedIn', 'true');
        this.authService.isLogged.next(true);


        this.router.navigate(['/posts']);
        console.log(response);
      });
    } 
  }

}
