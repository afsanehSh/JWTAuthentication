import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RolesComponent } from './roles/roles.component';
import { RolesUpdateComponent } from './roles/roles-update/roles-update.component';
import { UsersComponent } from './users/users.component';
import { UsersUpdateComponent } from './users/users-update/users-update.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UserManagementComponent } from './user-management.component';
import { UserManagementRoutingModule } from './user-management-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    UserManagementComponent,
    RolesComponent,
    RolesUpdateComponent,
    UsersComponent,
    UsersUpdateComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    UserManagementRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class UserManagementModule { }
