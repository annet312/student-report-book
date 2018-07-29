import { NgModule } from '@angular/core';

import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

import { CommonModule }       from '@angular/common';
 
import { myFocus } from '../../directives/focus.directive';
import {SpinnerComponent} from '../../spinner/spinner.component';  


@NgModule({
  imports: [CommonModule, NgbCollapseModule.forRoot()],
  declarations: [myFocus,SpinnerComponent],
  exports:      [myFocus,SpinnerComponent],
  providers:    []
})
export class SharedModule { }
