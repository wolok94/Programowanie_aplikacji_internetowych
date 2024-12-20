import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginModel } from '../../models/login-model';
import { Router } from '@angular/router';
import { MessageService } from '../../../shared/services/message.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(private authService : AuthService, private fb: FormBuilder, private router : Router, private messageService : MessageService) {
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
          let isAdmin = this.authService.checkRole();
          this.authService.isAdmin.next(isAdmin);
        }


        this.router.navigate(['/posts']);
        this.messageService.showMessage("Pomyślnie się zalogowano", "success");
      });
    } 
  }

}
