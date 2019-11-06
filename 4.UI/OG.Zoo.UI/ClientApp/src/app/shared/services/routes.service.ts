import { Injectable } from '@angular/core';
import { Routes, Route } from './routes';
import { Observable, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class RoutesService {

    routes = Routes;
    constructor() {
    }

    get(): Observable<Route[]> {
        return of(this.routes);
    }
}
