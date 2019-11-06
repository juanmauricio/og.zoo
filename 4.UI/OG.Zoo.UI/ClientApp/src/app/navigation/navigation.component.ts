import { Component, OnInit, OnDestroy } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { RoutesService } from '../shared/services/routes.service';
import { Route } from '../shared/services/routes';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { AuthService } from '../auth/login/services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  animations: [
    trigger('openClose', [
      state('open', style({ transform: 'rotate(0)' })),
      state('close', style({ transform: 'rotate(180deg)' })),
      transition('open => close', [animate('.2s')]),
      transition('close => open', [animate('.2s')])
    ])]
})
export class NavigationComponent implements OnInit, OnDestroy {
  routes: Route[];
  navigationVisible = false;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      untilComponentDestroyed(this)
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    private routesService: RoutesService,
    private authService: AuthService) { }

  ngOnInit() {
    this.routesService.get().pipe(untilComponentDestroyed(this)).subscribe((routes) => {
      this.routes = routes;
    });
    this.authService.authenticated.pipe(untilComponentDestroyed(this)).subscribe((value) => {
      this.navigationVisible = value;
      if (value) {
        this.authService.checkToken().pipe(untilComponentDestroyed(this)).subscribe();
      }
    });
  }

  close() {
    this.authService.deauthenticate();
    this.router.navigate(['/auth']);
  }

  ngOnDestroy(): void { }
}
