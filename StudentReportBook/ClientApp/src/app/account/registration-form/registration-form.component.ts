import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserRegistration } from '../../shared/models/user.registration.interface';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  isTeacher: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
  }

  registerUser({ value, valid }: { value: UserRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      console.log("register before calling");
      console.log(value);
      this.userService.register(value.email, value.password, value.firstName, value.lastName, value.role, value.department)
        .finally(() => {
          this.isRequesting = false;
        })
        .subscribe(
        result => {
          console.log(result);
          if (result) {
            this.router.navigate(['/login'], { queryParams: { brandNew: true, email: value.email } });
          }
          else {
            console.log("result is false");
          }
          },
        errors => {
          this.errors = errors;
          console.log(this.errors);
        });
    }
  }
  checkRole(event) {
    console.log(event);
    if (event.target.value == "Teacher") {
      this.isTeacher = true;
    }
    else {
      this.isTeacher = false;
    }
  }
}
