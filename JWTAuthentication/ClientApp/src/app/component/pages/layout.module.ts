import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PagesRoutingModule } from './pages-routing.module';
import { TopbarComponent } from './layout/topbar/topbar.component';
import { HeaderComponent } from './layout/header/header.component';
import { UserProfileComponent } from './layout/user-profile/user-profile.component';



@NgModule({
  declarations: [
    LayoutComponent,
    TopbarComponent,
    HeaderComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class LayoutModule { }
