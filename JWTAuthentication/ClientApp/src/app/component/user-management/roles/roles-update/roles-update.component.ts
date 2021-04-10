import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, Subscription } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { RoleModel } from '../../../../core/models/role.model';
import { RoleService } from '../../../../core/services/role/role.service';

const EMPTY_ROLE: RoleModel = {
  id: undefined,
  roleName: '',
  roleTitle: '',

};


@Component({
  selector: 'app-roles-update',
  templateUrl: './roles-update.component.html',
  styleUrls: ['./roles-update.component.css']
})
export class RolesUpdateComponent implements OnInit {

  id: number;
  role: RoleModel;
  previous: RoleModel;
  formGroup: FormGroup;
  errorMessage = '';
  tabs = {
    BASIC_TAB: 0,
  };
  activeTabId = this.tabs.BASIC_TAB; // 0 => Basic info | 1 => Remarks | 2 => Specifications
  private subscriptions: Subscription[] = [];

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.loadRole();
  }

  loadRole() {
    const sb = this.route.paramMap.pipe(
      switchMap(params => {
        // get id from URL
        this.id = Number(params.get('id'));
        if (this.id || this.id > 0) {
          return this.roleService.getRoleById(this.id);
        }
        return of(EMPTY_ROLE);
      }),
      catchError((errorMessage) => {
        this.errorMessage = errorMessage;
        return of(undefined);
      }),
    ).subscribe((res: RoleModel) => {
      if (!res) {
        this.router.navigate(['/user-management/roles'], { relativeTo: this.route });
      }

      this.role = res;
      this.previous = Object.assign({}, res);
      this.loadForm();
    });
    this.subscriptions.push(sb);
  }

  loadForm() {
    if (!this.role) {
      return;
    }

    this.formGroup = this.fb.group({
      roleName: [this.role.roleName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
      roleTitle: [this.role.roleTitle, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(40)])],
    });
  }

  reset() {
    if (!this.previous) {
      return;
    }

    this.role = Object.assign({}, this.previous);
    this.loadForm();
  }

  save() {
    this.formGroup.markAllAsTouched();
    if (!this.formGroup.valid) {
      return;
    }

    const formValues = this.formGroup.value;
    this.role = Object.assign(this.role, formValues);
    if (this.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.roleService.updateRole(this.role).pipe(
      tap(() => {
        let message = 'نقش با موفقیت ویرایش شد';
        //this.dialog.open(AlertDialogComponent, { data: { message }, height: '200px', width: '400px', disableClose: true });
        this.router.navigate(['/user-management/roleList']);
      }),
      catchError((errorMessage) => {
        console.error('UPDATE ERROR', errorMessage);
        return of(this.role);
      })
    ).subscribe(res => this.role = res as RoleModel);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.roleService.CreateRole(this.role).pipe(
      tap(() => {
        let message = 'نقش با موفقیت ایجاد شد';
        //this.dialog.open(AlertDialogComponent, { data: { message }, height: '200px', width: '400px', disableClose: true });
        this.router.navigate(['/user-management/roleList']);
      }),
      catchError((errorMessage) => {
        console.error('UPDATE ERROR', errorMessage);
        return of(this.role);
      })
    ).subscribe(res => this.role = res as RoleModel);
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
