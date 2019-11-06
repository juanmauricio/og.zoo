import { Directive, HostListener, Input } from '@angular/core';

@Directive({
// tslint:disable-next-line: directive-selector
  selector: '[debug]'
})
export class DebugDirective {

  constructor() { }

  @Input() debug: any;

  @HostListener('click') onclick() {
    console.log(this.debug);
  }

}
