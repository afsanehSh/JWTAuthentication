import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'oidc-client';
import { of, Subscription } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { UserModel } from '../../../../core/models/user.model';
import { UserService } from '../../../../core/services/user/user.service';

const EMPTY_USER: UserModel = {
  id: undefined,
  firstName: '',
  lastName: '',
  userName: '',
  userAddress: '',
  cityId: undefined,
  genderTypeId: undefined,
  educationDegreeId: undefined,
  dateOfBirth: null,
  email: '',
  fullname: '',
  password: '',
  isActive: false,
  isAdmin: false,
  jobTitle: '',
  mobile: '',
  personalCode: '',
  phoneNumber: '',
  userImage: '',
  userTitle: '',
};

@Component({
  selector: 'app-users-update',
  templateUrl: './users-update.component.html',
  styleUrls: ['./users-update.component.css']
})

export class UsersUpdateComponent implements OnInit {

  id: number;
  user: UserModel;
  previous: UserModel;
  formGroup: FormGroup;
  errorMessage = '';
  tabs = {
    BASIC_TAB: 0,
  };
  activeTabId = this.tabs.BASIC_TAB; // 0 => Basic info | 1 => Remarks | 2 => Specifications
  private subscriptions: Subscription[] = [];

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    const sb = this.route.paramMap.pipe(
      switchMap(params => {
        // get id from URL
        this.id = Number(params.get('id'));
        if (this.id || this.id > 0) {
          return this.userService.getUserById(this.id);
        }
        return of(EMPTY_USER);
      }),
      catchError((errorMessage) => {
        this.errorMessage = errorMessage;
        return of(undefined);
      }),
    ).subscribe((res: UserModel) => {
      if (!res) {
        this.router.navigate(['/user-management/userList'], { relativeTo: this.route });
      }

      this.user = res;
      this.previous = Object.assign({}, res);
      this.loadForm();
    });
    this.subscriptions.push(sb);
  }

  loadForm() {
    if (!this.user) {
      return;
    }

    this.formGroup = this.fb.group({
      firstName: [this.user.firstName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
      lastName: [this.user.lastName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
      userName: [this.user.userName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
      email: [this.user.email, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
      password: [this.user.password, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
    });
  }

  reset() {
    if (!this.previous) {
      return;
    }

    this.user = Object.assign({}, this.previous);
    this.loadForm();
  }

  save() {
    this.formGroup.markAllAsTouched();
    if (!this.formGroup.valid) {
      return;
    }

    const formValues = this.formGroup.value;
    this.user = Object.assign(this.user, formValues);
    if (this.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.userService.updateUser(this.user).pipe(
      tap(() => {
        let message = 'کاربر با موفقیت ویرایش شد';
        //this.dialog.open(AlertDialogComponent, { data: { message }, height: '200px', width: '400px', disableClose: true });
        this.router.navigate(['/user-management/userList']);
      }),
      catchError((errorMessage) => {
        console.error('UPDATE ERROR', errorMessage);
        return of(this.user);
      })
    ).subscribe(res => this.user = res as UserModel);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.userService.CreateUser(this.user).pipe(
      tap(() => {
        let message = 'کاربر با موفقیت ایجاد شد';
        //this.dialog.open(AlertDialogComponent, { data: { message }, height: '200px', width: '400px', disableClose: true });
        this.router.navigate(['/user-management/userList']);
      }),
      catchError((errorMessage) => {
        console.error('create ERROR', errorMessage);
        return of(this.user);
      })
    ).subscribe(res => this.user = res as UserModel);
    this.subscriptions.push(sbCreate);
  }

  changeTab(tabId: number) {
    this.activeTabId = tabId;
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }

  // helpers for View
  isControlValid(controlName: string): boolean {
    const control = this.formGroup.controls[controlName];
    return control.valid && (control.dirty || control.touched);
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.formGroup.controls[controlName];
    return control.invalid && (control.dirty || control.touched);
  }

  controlHasError(validation: string, controlName: string) {
    const control = this.formGroup.controls[controlName];
    return control.hasError(validation) && (control.dirty || control.touched);
  }

  isControlTouched(controlName: string): boolean {
    const control = this.formGroup.controls[controlName];
    return control.dirty || control.touched;
  }

}
