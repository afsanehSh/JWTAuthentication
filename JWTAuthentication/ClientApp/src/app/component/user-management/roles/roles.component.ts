import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RoleModel } from '../../../core/models/role.model';
import { RoleService } from '../../../core/services/role/role.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  roleList: RoleModel[];

  constructor(private roleService: RoleService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.getRoleList();
  }

  getRoleList() {
    this.roleService.getRoleList()
      .subscribe((data: RoleModel[]) => {
        this.roleList = data
        this.cdr.detectChanges();
      });
  }

  deleteRole(role: RoleModel) {
    if (confirm("آیا برای حذف اطمینان دارید ")) {
      console.log("are you sure for delete item");
      this.roleService.deleteRole(role.id)
        .subscribe(data => {
          this.roleList = this.roleList.filter(u => u !== role);
        })
    }
  }

  addRole() {
    this.router.navigate(['/user-management/role/add']);
  }

  updateRole(id) {
    this.router.navigate(['/user-management/role/edit', id]);
  }

}
