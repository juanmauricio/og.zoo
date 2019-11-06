import { MatToolbarModule } from '@angular/material/toolbar';
import { NgModule } from '@angular/core';
import { LayoutModule } from '@angular/cdk/layout';
import {
  MatButtonModule, MatSidenavModule, MatIconModule, MatListModule,
  MatTableModule, MatPaginatorModule, MatSortModule, MatInputModule,
  MatSelectModule, MatRadioModule, MatCardModule, MatSnackBarModule,
  MatDialogModule, MatFormFieldModule, MatAutocompleteModule,
  MatProgressBarModule, MatProgressSpinnerModule
} from '@angular/material';
import { ConfirmComponent } from './dialogs/confirm/confirm.component';
import { LoadingComponent } from './loading/loading.component';
import { InputDialogComponent } from './dialogs/input-dialog/input-dialog.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { DebugDirective } from './directives/debug.directive';
import { FormComponent } from './components/form/form.component';


@NgModule({
  declarations: [ConfirmComponent, LoadingComponent, InputDialogComponent, DebugDirective, FormComponent],
  exports: [
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatIconModule,
    MatSnackBarModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    ConfirmComponent,
    FormComponent,
    DebugDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatCardModule
  ]
})
export class SharedModule { }
