import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private name: string = null;

  private IsLoggedIn = false;

  constructor(private auth: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.IsLoggedIn = this.auth.isAuthenticated();
    if (this.IsLoggedIn) {
      this.name = this.auth.getCurrentUser();
    }
  }
  submitlogout() {
    this.name = null;
    this.userService.logout();
    this.IsLoggedIn = false;
    }
  
}
