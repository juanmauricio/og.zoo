import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/generics/base-service';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/auth/login/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<User> {
  constructor(http: HttpClient, snackBar: MatSnackBar, authService: AuthService) {
    super(http, 'user', snackBar, authService);
  }
}
