import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginModel } from '../../models/login-model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(private authService : AuthService, private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]], 

    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      let login: LoginModel = this.loginForm.value;
      console.log(login);
      this.authService.login(login).subscribe(response => {
        console.log(response);
      });
    } 
  }

}
