import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../modules/shared/shared.module';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [SharedModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  isLogged = false;

  ngOnInit(): void {
    this.authService.isLogged.subscribe(value => {
      this.isLogged = value;
    })
  }

  constructor(private authService : AuthService) {
    
  }



}
