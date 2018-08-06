import { Component, OnInit } from '@angular/core';
import { GradeBookComponent } from './gradebook/gradebook.component';
import { StudentsComponent } from './students/students.component';
import { TeacherComponent } from './teacher/teacher.component';
import { UserService } from '../shared/services/user.service';

import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'start-work',
  templateUrl: './start-work.component.html',
})
export class StartWorkComponent implements OnInit {
  title = 'start-work';

  constructor(private userService: UserService, private router: Router) {
  }

  ngOnInit() {
    let role = localStorage.getItem('current_role');
    switch (role) {
      case "Student": {
        this.router.navigate(['gradebook']);
        break;
      }
      case "Teacher": {
        this.router.navigate(['student']);
        break;
      }
      case "Moderator": {
        this.router.navigate(['teacher']);
        break;
      }
      default: {
        this.router.navigate(['/']);
        break;
      }
    } 
 }
}
