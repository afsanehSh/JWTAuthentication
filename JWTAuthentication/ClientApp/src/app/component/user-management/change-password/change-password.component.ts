import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { UserModel } from '../../../core/models/user.model';
import { AuthService } from '../../../core/services/auth/auth.service';
import { UserService } from '../../../core/services/user/user.service';
import { ConfirmPasswordValidator } from '../../auth/registration/confirm-password.validator';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  formGroup: FormGroup;
  user: UserModel;
  firstUserState: UserModel;
  subscriptions: Subscription[] = [];
  previous: any;

  constructor(private authService: AuthService,
    private userService: UserService,
    private fb: FormBuilder) {
  }

  ngOnInit(): void {
    const sb = this.authService.currentUserSubject.asObservable().pipe(
      first(user => !!user)
    ).subscribe(user => {
      this.user = Object.assign({}, user);
      this.firstUserState = Object.assign({}, user);
      this.loadForm();
    });
    this.subscriptions.push(sb);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }

  loadForm() {
    this.formGroup = this.fb.group({
      currentPassword: [this.user.password, Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }
      , {
        validator: ConfirmPasswordValidator.MatchPassword
      }
    );
  }

  save() {
    this.formGroup.markAllAsTouched();
    if (!this.formGroup.valid) {
      return;
    }
    this.userService.changePassword(this.formGroup.value).subscribe((data: UserModel) => {
      console.log(data);
      alert('رمزعبور با موفقیت تغییر کرد');
    }, (error: any) => {
      let errorMessage = "خطایی به وجود آمده است";
      console.log(errorMessage);
      alert(errorMessage);
    });
  }

  reset() {
    if (!this.previous) {
      return;
    }

    this.user = Object.assign({}, this.previous);
    this.loadForm();
  }

  cancel() {
    this.user = Object.assign({}, this.firstUserState);
    this.loadForm();
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

  controlHasError(validation, controlName): boolean {
    const control = this.formGroup.controls[controlName];
    return control.hasError(validation) && (control.dirty || control.touched);
  }

  isControlTouched(controlName): boolean {
    const control = this.formGroup.controls[controlName];
    return control.dirty || control.touched;
  }

}
