import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';

const routes: Route[] = [
  { path: 'animals', loadChildren: './animals/animals.module#AnimalsModule' },
  { path: 'infirmaries', loadChildren: './infirmaries/infirmaries.module#InfirmariesModule' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class ParamsModule { }
