import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';

import { CommonModule }       from '@angular/common';
 
import { myFocus } from '../../directives/focus.directive';
import {SpinnerComponent} from '../../spinner/spinner.component';  


@NgModule({
  imports: [CommonModule, MatTabsModule],
  declarations: [myFocus,SpinnerComponent],
  exports:      [myFocus,SpinnerComponent],
  providers:    []
})
export class SharedModule { }
