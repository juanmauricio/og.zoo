import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { Route, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { InputDialogComponent } from '../shared/dialogs/input-dialog/input-dialog.component';
import { LoginComponent } from './login/login.component';
import { RecoveryComponent } from './recovery/recovery.component';
import { RecoveryGuardService } from './recovery/services/recovery-guard.service';

const routes: Route[] = [
    { path: '', component: LoginComponent },
    { path: 'recoveryPassword', component: RecoveryComponent, canActivate: [RecoveryGuardService] }

];

@NgModule({
  declarations: [LoginComponent, RecoveryComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [RecoveryGuardService],
  entryComponents: [InputDialogComponent]
})
export class AuthModule { }
