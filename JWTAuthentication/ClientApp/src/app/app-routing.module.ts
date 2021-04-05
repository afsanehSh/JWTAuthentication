import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { CounterComponent } from './component/counter/counter.component';
import { FetchDataComponent } from './component/fetch-data/fetch-data.component';
import { AuthGuardService } from './component/auth/services/authGuard/auth-guard.service';
import { LoginComponent } from './component/auth/login/login.component';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('../app/component/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    canActivate: [AuthGuardService],
    loadChildren: () =>
      import('../app/component/pages/layout.module').then((m) => m.LayoutModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
