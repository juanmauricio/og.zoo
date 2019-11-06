import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { AuthService } from '../login/services/auth.service';
import { UserLogin } from '../login/models/user';
import { FormResponse, FormConfig } from 'src/app/shared/components/form/form.component';
import { Validators } from '@angular/forms';
import { Base64 } from 'src/app/shared/utils/base64';
import { MatSnackBar } from '@angular/material';
import { CustomValidators } from 'src/app/shared/generics/custom-validators';
import { LoadingService } from 'src/app/shared/loading/loading.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-recovery',
  templateUrl: './recovery.component.html',
  styleUrls: ['./recovery.component.scss']
})
export class RecoveryComponent implements OnInit, OnDestroy {
  user: UserLogin;
  title: string;
  config: FormConfig = {
    fields: [{
      name: 'newPassword',
      label: 'New password',
      type: 'password',
      validators: {
        required: {
          validator: Validators.required,
          message: 'New password is <strong>required</strong>'
        }
      }
    }, {
      name: 'repeatPassword',
      label: 'Repeat password',
      type: 'password',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Repeat password is <strong>required</strong>'
        }
      }
    }],
    validators: {
      mustBeEquals: {
        validator: CustomValidators.mustBeEquals('newPassword', 'repeatPassword'),
        message: 'The passwords must be equals'
      }
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private loadingService: LoadingService,
    private authService: AuthService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.route.queryParams.pipe(untilComponentDestroyed(this)).subscribe((data) => {
      const tokenUser = { token: data.token || null, id: data.id || null };
      this.authService.checkRecoveryToken(tokenUser).subscribe(user => {
        this.user = user;
        this.user.token = tokenUser.token;
        this.title = `Welcome back <strong>${this.user.name}<strong>!`;
      }, () => {
        this.router.navigate(['/auth']);
      });
    });
  }

  onSubmit(data: FormResponse) {
    if (data.newPassword) {
      this.loadingService.show();
      this.user.password = Base64.encode(data.newPassword);
      this.authService.updatePassword(this.user).pipe(finalize(() => this.loadingService.hide())).subscribe(() => {
        this.router.navigate(['/auth']);
        this.snackBar.open('The password has been updated');
      });
    }
  }

  onCancel() {
    this.router.navigate(['/auth']);
  }

  ngOnDestroy(): void { }
}

