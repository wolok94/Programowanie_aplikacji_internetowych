import { Component } from '@angular/core';
import { PostManagementComponent } from "../post-management/post-management.component";
import { UserManagementComponent } from "../user-management/user-management.component";
import { AdminModule } from '../../modules/admin/admin.module';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [PostManagementComponent, UserManagementComponent, AdminModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {

  activeSection: 'posts' | 'users' = 'posts';

  showSection(section: 'posts' | 'users'): void {
    this.activeSection = section;
  }
}
