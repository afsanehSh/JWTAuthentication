import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './user-management.component';
import { RolesComponent } from './roles/roles.component';
import { RolesUpdateComponent } from './roles/roles-update/roles-update.component';
import { UsersComponent } from './users/users.component';
import { UsersUpdateComponent } from './users/users-update/users-update.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  {
    path: '',
    component: UserManagementComponent,
    children: [
      {
        path: 'roleList',
        component: RolesComponent
      },
      {
        path: 'role/add',
        component: RolesUpdateComponent,
      },
      {
        path: 'role/edit/:id',
        component: RolesUpdateComponent,
      },
      {
        path: 'userList',
        component: UsersComponent
      },
      {
        path: 'user/add',
        component: UsersUpdateComponent,
      },
      {
        path: 'user/edit/:id',
        component: UsersUpdateComponent,
      },
      {
        path: 'change-password/:id',
        component: ChangePasswordComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule { }
