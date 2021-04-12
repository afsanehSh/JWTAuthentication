import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserModel } from '../../../core/models/user.model';
import { UserService } from '../../../core/services/user/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  userList: UserModel[];

  constructor(private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.getUserList();
  }

  getUserList() {
    this.userService.getUserList()
      .subscribe((data: UserModel[]) => {
        this.userList = data
        this.cdr.detectChanges();
      });
  }

  deleteUser(role: UserModel) {
    if (confirm("آیا برای حذف اطمینان دارید ")) {
      console.log("are you sure for delete item");
      this.userService.deleteUser(role.id)
        .subscribe(data => {
          this.userList = this.userList.filter(u => u !== role);
        })
    }
  }

  addUser() {
    this.router.navigate(['/user-management/user/add']);
  }

  updateUser(id) {
    this.router.navigate(['/user-management/user/edit', id]);
  }

}
