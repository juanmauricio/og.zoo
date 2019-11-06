import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './auth/login/services/auth-guard.service';

const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'auth', loadChildren: './auth/auth.module#AuthModule', canActivate: [AuthGuardService] },
  { path: 'home', loadChildren: './home/home.module#HomeModule', canActivate: [AuthGuardService] },
  { path: 'security', loadChildren: './zoo/security/security.module#SecurityModule', canActivate: [AuthGuardService] },
  { path: 'params', loadChildren: './zoo/params/params.module#ParamsModule', canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuardService]
})
export class AppRoutingModule { }
