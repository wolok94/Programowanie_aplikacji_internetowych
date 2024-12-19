import { Component, OnInit } from '@angular/core';
import { AdminModule } from '../../modules/admin/admin.module';
import { UserService } from '../../services/user.service';
import { GetUsersModel } from '../../models/get-users-model';
import { FormsModule } from '@angular/forms';
import { RoleModel } from '../../models/role-model';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [AdminModule, FormsModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit {

  users: GetUsersModel[] = [];
  roles: RoleModel[] = [];

  constructor(private userService: UserService) {
    
  }
  ngOnInit(): void {
    this.userService.getUsers().subscribe(response => {
      console.log(response);
      this.users = response;
    })

    this.userService.getRoles().subscribe(response => {
      this.roles = response;
    })
  }

  updateUserRole(userId: string): void {
    console.log(`Zapisano zmiany dla użytkownika o ID: ${userId}`);
  }

}
