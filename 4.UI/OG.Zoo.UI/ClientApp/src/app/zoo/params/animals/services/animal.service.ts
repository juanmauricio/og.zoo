import { Injectable } from '@angular/core';
import { Animal } from '../models/animal';
import { BaseService } from 'src/app/shared/generics/base-service';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { AuthService } from 'src/app/auth/login/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AnimalService extends BaseService<Animal> {
  constructor(http: HttpClient, snackBar: MatSnackBar, authService: AuthService) {
    super(http, 'animal', snackBar, authService);
  }
}
