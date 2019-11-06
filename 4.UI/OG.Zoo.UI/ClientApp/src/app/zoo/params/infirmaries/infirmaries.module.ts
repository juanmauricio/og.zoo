import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { InfirmariesComponent } from './infirmaries.component';
import { InfirmariesListComponent } from './infirmaries-list/infirmaries-list.component';
import { InfirmaryComponent } from './infirmary/infirmary.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Route } from '@angular/router';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';

const routes: Route[] = [
  { path: '', component: InfirmariesComponent },
  { path: 'create', component: InfirmaryComponent },
  { path: 'update/:id', component: InfirmaryComponent }
];

@NgModule({
  declarations: [InfirmariesComponent, InfirmariesListComponent, InfirmaryComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  entryComponents: [ConfirmComponent],
  providers: [DatePipe]
})
export class InfirmariesModule { }
