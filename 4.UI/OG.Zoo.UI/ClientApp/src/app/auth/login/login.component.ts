import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { MatSnackBar, MatDialog } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { InputDialogData, InputDialogComponent, InputDialogResponse } from 'src/app/shared/dialogs/input-dialog/input-dialog.component';
import { LoadingService } from 'src/app/shared/loading/loading.service';
import { Base64 } from 'src/app/shared/utils/base64';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  authForm = this.fb.group({
    name: [null, Validators.required],
    password: [null, Validators.required]
  });

  recoveryPasswordData: InputDialogData = {
    content: 'Please enter your email',
    fields: [
      {
        label: 'Email',
        name: 'email',
        validators: {
          required: {
            message: 'Email is <strong>required</strong>',
            validator: Validators.required
          },
          email: {
            message: 'Must be a <strong>valid</strong> email',
            validator: Validators.email
          }
        },
        icon: 'mail_outline'
      }
    ]
  };

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.route.queryParams.pipe(untilComponentDestroyed(this)).subscribe((data) => {
      if (data.recovery) {
        this.openRecoveryPassword();
      }
    });
  }

  onSubmit() {
    if (this.authForm.valid) {
      this.loadingService.show();
      const user = this.authForm.value;
      user.password = Base64.encode(user.password);
      this.authService.authenticate(user).pipe(untilComponentDestroyed(this), finalize(() => this.loadingService.hide())).subscribe(() => {
        this.snackBar.open(`User "${user.name}" has logged on.`, 'Dismiss', { duration: 3000 });
        this.router.navigate(['home']);
      });
    }
  }

  openRecoveryPassword() {
    const dialogRef = this.dialog.open(InputDialogComponent, { width: '350px', data: this.recoveryPasswordData });
    dialogRef.beforeClosed().pipe(untilComponentDestroyed(this)).subscribe((result: InputDialogResponse) => {
      if (result) {
        this.loadingService.show();
        this.authService.sendRecovery(result.email).pipe(
          untilComponentDestroyed(this),
          finalize(() => this.loadingService.hide())
        ).subscribe(res => {
          this.snackBar.open(`Please check your mailbox`, 'Dismiss', { duration: 3000 });
        });
      }
    });
  }

  ngOnDestroy(): void { }

}
