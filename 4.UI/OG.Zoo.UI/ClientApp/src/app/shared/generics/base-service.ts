import { BaseEntity } from './base-entity';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from './response';
import { map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';
import { Paginated } from './paginated';
import { AuthService } from 'src/app/auth/login/services/auth.service';

/**
 * A base for the services
 *
 * @export
 */
export class BaseService<TEntity extends BaseEntity> {
  api: string;

  constructor(
    protected http: HttpClient,
    protected urlController: string,
    protected snackBar: MatSnackBar,
    protected authService: AuthService
  ) {
    this.api = environment.apiZoo;
  }

  /**
   * Get the options of the request
   *
   * @param [params] Dynamic params
   * @returns Return the options of the request
   */
  protected getOptions(params?: { [x: string]: any }) {
    return {
      headers: {
        Authorization: `Bearer ${this.authService.getToken()}`
      },
      params: params ? params : {}
    };
  }

  /**
   * Handle response
   */
  protected handleResponse<TResult>() {
    return map((response: Response<TResult>) => {
      if (!response.isSuccess) {
        this.snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
        throw new Error(response.exceptionMessage);
      }
      return response.result;
    });
  }

  /**
   * Get all the entities
   *
   * @param [urlController] The url controller
   * @returns A observable with a array of entities
   */
  public getAll(urlController?: string): Observable<TEntity[]> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
    return this.http.get<Response<TEntity[]>>(`${this.api}/${controller}/`, this.getOptions())
      .pipe(this.handleResponse<TEntity[]>());
  }

  /**
   * Get all the entities
   *
   * @param [urlController] The url controller
   * @returns A observable with a array of entities
   */
  public getPaginated(params: { [x: string]: any }, urlController?: string): Observable<Paginated<TEntity>> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : `${this.urlController}/paginated`;
    return this.http.get<Response<Paginated<TEntity>>>(`${this.api}/${controller}/`, this.getOptions(params))
      .pipe(this.handleResponse<Paginated<TEntity>>());
  }

  /**
   * Get the entity by id
   *
   * @param id The entity id
   * @param [urlController] The url controller
   * @returns A observable with the entity
   */
  public get(id: string, urlController?: string): Observable<TEntity> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
    return this.http.get<Response<TEntity>>(`${this.api}/${controller}/${id}`, this.getOptions())
      .pipe(this.handleResponse<TEntity>());
  }

  /**
   * Create a entity
   *
   * @param entity The entity to create
   * @param [urlController] The url controller
   * @returns A observable with the entity
   */
  public create(entity: TEntity, urlController?: string): Observable<TEntity> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
    return this.http.post<Response<TEntity>>(`${this.api}/${controller}/`, entity, this.getOptions())
      .pipe(this.handleResponse<TEntity>());
  }

  /**
   * Update a entity
   *
   * @param entity The entity to update
   * @param [urlController] The url controller
   * @returns A observable with the entity
   */
  public update(entity: TEntity, urlController?: string): Observable<TEntity | any> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
    return this.http.put<Response<TEntity>>(`${this.api}/${controller}/`, entity, this.getOptions())
      .pipe(this.handleResponse<TEntity>());
  }

  /**
   * Delete a entity
   *
   * @param id The entity id
   * @param [urlController] The url controller
   * @returns A observable with the entity
   */
  public delete(id: string, urlController?: string): Observable<TEntity> {
    const controller = typeof (urlController) !== 'undefined' ? urlController : this.urlController;
    return this.http.delete<Response<TEntity>>(`${this.api}/${controller}/${id}`, this.getOptions())
      .pipe(this.handleResponse<TEntity>());
  }
}
