import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../modules/shared/shared.module';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { MessageService } from '../../services/message.service';

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

  constructor(private authService : AuthService, private router : Router, private messageService : MessageService) {
    
  }

  logout() {
    this.authService.removeTokens();
    this.router.navigate(['/login']);
    this.messageService.showMessage("Zostałeś wylogowany", "info");
  }



}
