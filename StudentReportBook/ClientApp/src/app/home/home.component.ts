import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private name: string = null;
  private IsLoggedIn = false;
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.IsLoggedIn = this.userService.isLoggedIn();
    if (this.IsLoggedIn) {
      this.name = this.userService.getCurrentUser();
    }
  }
  submitlogout() {
    this.name = null;
    this.userService.logout();
    this.IsLoggedIn = false;
    }
  
}
