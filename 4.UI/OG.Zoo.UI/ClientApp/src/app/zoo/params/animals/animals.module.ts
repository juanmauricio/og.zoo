import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnimalsComponent } from './animals.component';
import { AnimalsListComponent } from './animals-list/animals-list.component';
import { AnimalComponent } from './animal/animal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Route } from '@angular/router';
import { ConfirmComponent } from 'src/app/shared/dialogs/confirm/confirm.component';

const routes: Route[] = [
  { path: '', component: AnimalsComponent },
  { path: 'create', component: AnimalComponent },
  { path: 'update/:id', component: AnimalComponent }
];

@NgModule({
  declarations: [AnimalsComponent, AnimalsListComponent, AnimalComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
  entryComponents: [ConfirmComponent]
})
export class AnimalsModule { }
