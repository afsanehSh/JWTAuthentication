import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { ExceptionModel } from '../../../core/models/exception.model';
import { AuthService } from '../../../core/services/auth/auth.service';
import { ConfirmPasswordValidator } from './confirm-password.validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup;
  hasError: boolean;
  returnUrl: string;

  // private fields
  private unsubscribe: Subscription[] = [];
  loading: boolean;
  errorMessageUserName: any;
  errorMessagePhoneNumber: any;
  errorMessageEmail: any;
  password: boolean;
  confirmPassword: boolean;
  showInitWaitingMessage: boolean = false;
  errorMessage: string;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
  ) {
    // redirect to home if already logged in
    if (this.authService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.initForm();
    this.returnUrl =
      this.route.snapshot.queryParams['returnUrl'.toString()] || '/';
  }

  get f() {
    return this.registrationForm.controls;
  }

  initForm() {
    this.registrationForm = this.fb.group(
      {
        firstName: [
          '',
          Validators.compose([
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(40),
          ]),
        ],
        lastName: [
          '',
          Validators.compose([
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(40),
          ]),
        ],
        userName: [
          '',
          Validators.compose([
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(20),
          ]),
        ],
        email: [
          '',
          Validators.compose([
            Validators.email,
            //Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$"),
          ]),
        ],
        password: [
          '',
          Validators.compose([
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern(/^([0-9]+[a-zA-Z]+|[a-zA-Z]+[0-9]+)[0-9a-zA-Z]*$/)
          ]),
        ],
        confirmPassword: [
          '',
          Validators.compose([
            Validators.required,
          ]),
        ],
      }, {
      validator: ConfirmPasswordValidator.MatchPassword
    }
    );
  }

  submit() {
    this.cleanErrors();
    this.hasError = false;
    this.showInitWaitingMessage = true;
    const registrationSubscr = this.authService
      .registration(this.registrationForm.value)
      .pipe(first())
      .subscribe(user => {
        if (user) {
          this.showInitWaitingMessage = false;
          this.router.navigate(['/']);
        }
        //else {
        //    this.hasError = true;
        //}
      },
        (err: ExceptionModel) => {
          this.showInitWaitingMessage = false;
          this.errorSubmit(err);
        });
    this.unsubscribe.push(registrationSubscr);
  }

  errorSubmit(error) {
    if (error.error.exceptionHint == 'PhoneNumberIsDuplicate') {
      this.errorMessagePhoneNumber = error.error.message;
    }
    else if (error.error.exceptionHint == 'EmailIsDuplicate') {
      this.errorMessageEmail = error.error.message;
    }
    else if (error.error.exceptionHint == 'UserIsDuplicate') {
      this.errorMessageUserName = error.error.message;
    } else {
      this.errorMessage = 'خطایی به وجود آمده';
    }
    this.cdr.detectChanges();
  }

  cleanErrors() {
    this.errorMessage = '';
    this.errorMessageEmail = '';
    this.errorMessagePhoneNumber = '';
    this.errorMessageUserName = '';
  }

  ngOnDestroy() {
    this.unsubscribe.forEach((sb) => sb.unsubscribe());
  }

  togglePassword() {
    this.password = !this.password;
  }

  toggleConfirmPassword() {
    this.confirmPassword = !this.confirmPassword;
  }

}
