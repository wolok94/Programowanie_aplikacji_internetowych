import { Component, OnInit } from '@angular/core';
import { AdminModule } from '../../modules/admin/admin.module';
import { UserService } from '../../services/user.service';
import { GetUsersModel } from '../../models/get-users-model';
import { FormsModule } from '@angular/forms';
import { RoleModel } from '../../models/role-model';
import { ChangeRole } from '../../models/change-role-model';
import { MessageService } from '../../../shared/services/message.service';

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

  constructor(private userService: UserService, private messageService: MessageService) {
    
  }
  ngOnInit(): void {
    this.getUsers();

    this.userService.getRoles().subscribe(response => {
      this.roles = response;
    })
  }

  getUsers() {
    this.userService.getUsers().subscribe(response => {
      console.log(response);
      this.users = response;
    })
  }

  updateUserRole(user: GetUsersModel): void {
    let changeRole: ChangeRole = {
      roleId : user.role.id,
      userId : user.id
    }
    this.userService.updateUser(changeRole).subscribe({
      next: () => {
        this.getUsers();
        this.messageService.showMessage("Pomyślnie zaktualizowano rolę", "success");
      }, error: (err) => {
        this.messageService.showMessage("Wystąpił błąd podczas dodawania roli. Spróbuj ponownie.", "error");
     }

    })
  }

}
