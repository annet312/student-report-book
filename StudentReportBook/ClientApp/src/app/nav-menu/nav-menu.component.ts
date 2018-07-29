import { Component, Input } from '@angular/core';
import { UserResponse } from '../shared/models/UserResponse';
import { Subscription } from 'rxjs';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
  public isCollapsed = false;


  ngOnInit() {
  }
}
