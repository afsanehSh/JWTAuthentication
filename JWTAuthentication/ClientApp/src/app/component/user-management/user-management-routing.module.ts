import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './user-management.component';
import { RolesComponent } from './roles/roles.component';
import { RolesUpdateComponent } from './roles/roles-update/roles-update.component';

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
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule { }
