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
        console.log(response);
        const accessToken = response.headers.get("Access-Token");
        const refreshToken = response.headers.get("Refresh-Token");
        const accessTokenExpiresAt = response.headers.get("Access-Token-ExpiresAt");
        const refreshTokenExpiresAt = response.headers.get("Refresh-Token-ExpiresAt");

        if(accessToken && refreshToken && accessTokenExpiresAt && refreshTokenExpiresAt){
          this.authService.saveTokens(accessToken, refreshToken, accessTokenExpiresAt, refreshTokenExpiresAt);
        }


        this.router.navigate(['/posts']);
        console.log(response);
      });
    } 
  }

}
