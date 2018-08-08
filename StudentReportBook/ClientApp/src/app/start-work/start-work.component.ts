import { Component, OnInit } from '@angular/core';
import { GradeBookComponent } from './gradebook/gradebook.component';
import { TeacherComponent } from './teacher/teacher.component';
import { ModeratorComponent } from './moderator/moderator.component';
import { AuthService } from '../auth/auth.service';

import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'start-work',
  templateUrl: './start-work.component.html',
})
export class StartWorkComponent implements OnInit {
  title = 'start-work';

  constructor( private router: Router, private auth: AuthService) {
  }

  ngOnInit() {
    let role = this.auth.getCurrentUserRole();
    switch (role) {
      case "Student": {
        this.router.navigate(['gradebook']);
        break;
      }
      case "Teacher": {
        this.router.navigate(['teacher']);
        break;
      }
      case "Moderator": {
        this.router.navigate(['moderator']);
        break;
      }
      default: {
        this.router.navigate(['/']);
        break;
      }
    } 
 }
}
