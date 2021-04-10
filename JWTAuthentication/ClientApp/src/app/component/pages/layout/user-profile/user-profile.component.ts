import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserModel } from '../../../../core/models/user.model';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  extrasUserOffcanvasDirection = 'offcanvas-right';
  user$: Observable<UserModel>;
  currentUser: UserModel;
  userProfileImage: any;

  constructor(
    private auth: AuthService,
    private cdr: ChangeDetectorRef,
    private router: Router) { }

  ngOnInit(): void {
    this.currentUser = this.auth.currentUserValue;

    //this.getUserProfileImg();
  }

  logout() {
    this.auth.logout();
    document.location.reload();
  }

  getUserProfileImg() {
  //  this.auth.getUserProfileImage()
  //    .subscribe((data: any) => {
  //      this.loadImage(data);
  //    }, (error: any) => {
  //      console.log(error)
  //    });
  }

  loadImage(img: File) {
  //  const mimeType = img.type;
  //  if (mimeType.match(/image\/*/) == null) {
  //    return;
  //  }
  //  const reader = new FileReader();
  //  reader.readAsDataURL(img);
  //  reader.onload = (_event) => {
  //    this.userProfileImage = reader.result;
  //    this.cdr.detectChanges();
  //  }
  }

  editUserProfile() {
  //  this.router.navigate(['./user-management/app-user/edit/' + this.currentUser.id]);
  }

  changePassword() {
  //  this.router.navigate(['./user-management/change-password/' + this.currentUser.id]);
  }

}
