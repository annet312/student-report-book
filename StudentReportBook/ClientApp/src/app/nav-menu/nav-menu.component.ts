import { Component, Input } from '@angular/core';
//import { UserService } from '../shared/services/user.service';
import { UserResponse } from '../shared/models/UserResponse';
import { Subscription } from 'rxjs';
//import { NgOnChangesFeature } from '../../../node_modules/@angular/core/src/render3';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
//  providers: [UserService]
})
export class NavMenuComponent {

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  constructor(/*private userService: UserService*/) { };

  ngOnInit() {
  }
}
