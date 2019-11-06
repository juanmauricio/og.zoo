import { environment } from 'src/environments/environment';
import { HttpClient, } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';
import { Injectable } from '@angular/core';
import { UserLogin } from '../models/user';
import { Response } from '../../../shared/generics/response';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private isAuthenticatedSource = new BehaviorSubject(localStorage.getItem(environment.key) != null);
    authenticated = this.isAuthenticatedSource.asObservable();
    api: string;
    key: string;

    constructor(private http: HttpClient, private snackBar: MatSnackBar) {
        this.api = environment.apiZoo;
        this.key = environment.key;
    }

    /**
     * Get the options of the request
     *
     * @returns Return the options of the request
     */
    protected getOptions() {
        return {
            headers: {
                Authorization: `Bearer ${this.getToken()}`
            }
        };
    }

    /**
     * Set if user authenticated
     *
     */
    public setAuthenticated(status: boolean) {
        this.isAuthenticatedSource.next(status);
    }

    /**
     * Get if user authenticated
     *
     * @returns true if user is authenticated
     */
    public isAuthenticated(): boolean {
        return localStorage.getItem(environment.key) != null;
    }

    /**
     * Checks the token
     *
     * @returns A observable with true or false
     */
    public checkToken(): Observable<boolean> {
        return this.http.get<Response<boolean>>(`${this.api}/user/checkToken`, this.getOptions()).pipe(
            map(res => res.result));
    }

    public checkRecoveryToken(user: UserLogin): Observable<UserLogin> {
        return this.http.post<Response<UserLogin>>(`${this.api}/user/checkRecoveryToken`, user).pipe(
            tap((response) => {
                if (!response.isSuccess) {
                    this.snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
                    throw new Error(response.exceptionMessage);
                }
            }),
            map(res => res.result)
        );
    }

    /**
     * Create a entity
     *
     * @param entity The entity to create
     * @param [urlController] The url controller
     * @returns A observable with the entity
     */
    public authenticate(entity: UserLogin): Observable<UserLogin> {
        return this.http.post<Response<UserLogin>>(`${this.api}/user/login`, entity).pipe(tap((response) => {
            if (!response.isSuccess) {
                this.snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
                throw new Error(response.exceptionMessage);
            } else {
                localStorage.setItem(this.key, JSON.stringify(response.result));
                this.setAuthenticated(true);
            }
        }), map(response => response.result));
    }

    public sendRecovery(email: string): Observable<boolean> {
        return this.http.get<Response<boolean>>(`${this.api}/user/sendRecovery`, { params: { email } }).pipe(tap((response) => {
            if (!response.isSuccess) {
                this.snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
                throw new Error(response.exceptionMessage);
            }
        }), map(response => response.result));
    }

    public updatePassword(user: UserLogin): Observable<UserLogin> {
        return this.http.post<Response<UserLogin>>(`${this.api}/user/updatePassword`, user).pipe(
            tap((response) => {
                if (!response.isSuccess) {
                    this.snackBar.open(response.exceptionMessage, 'Dismiss', { duration: 3000 });
                    throw new Error(response.exceptionMessage);
                }
            }),
            map(res => res.result)
        );
    }

    /**
     * Get Token
     */
    public getToken() {
        if (this.isAuthenticated()) {
            const user: UserLogin = JSON.parse(localStorage.getItem(this.key));
            return user.token;
        } else {
            return '';
        }
    }

    /**
     * Deauthenticate the user
     *
     */
    public deauthenticate(): void {
        this.setAuthenticated(false);
        localStorage.clear();
    }
}
