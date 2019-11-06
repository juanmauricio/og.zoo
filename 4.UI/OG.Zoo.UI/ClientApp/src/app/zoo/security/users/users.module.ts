import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserComponent } from './user/user.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Route } from '@angular/router';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';

const routes: Route[] = [
  { path: '', component: UsersComponent },
  { path: 'create', component: UserComponent },
  { path: 'update/:id', component: UserComponent }
];

@NgModule({
  declarations: [UsersComponent, UsersListComponent, UserComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  entryComponents: [ConfirmComponent]
})
export class UsersModule { }
