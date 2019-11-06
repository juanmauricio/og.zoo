import { Injectable } from '@angular/core';
import { Infirmary } from '../models/infirmary';
import { BaseService } from 'src/app/shared/generics/base-service';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';
import { Paginated } from 'src/app/shared/generics/paginated';
import { AuthService } from 'src/app/auth/login/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class InfirmaryService extends BaseService<Infirmary> {
  constructor(http: HttpClient, snackBar: MatSnackBar, authService: AuthService) {
    super(http, 'infirmary', snackBar, authService);
  }

  /**
   * Get all the entities
   *
   * @param [urlController] The url controller
   * @returns A observable with a array of entities
   */
  public getAllWithRelations(params: { [x: string]: any }): Observable<Paginated<Infirmary>> {
    return this.getPaginated(params, `${this.urlController}/getAllWithRelations`);
  }

  /**
   * Get the entity by id
   *
   * @param id The entity id
   * @param [urlController] The url controller
   * @returns A observable with the entity
   */
  public getWithRelations(id: string): Observable<Infirmary> {
    return this.get(id, `${this.urlController}/getWithRelations`);
  }
}
