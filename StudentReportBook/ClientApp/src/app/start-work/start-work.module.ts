import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { NgbModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';


import { UserService } from '../shared/services/user.service';
import { routing } from './start-work.routing';

import { GradeBookComponent } from './gradebook/gradebook.component';
import { StudentsComponent } from './students/students.component';
import { TeacherComponent } from './teacher/teacher.component';
import { StartWorkComponent } from './start-work.component';




@NgModule({
  imports: [
    CommonModule, FormsModule, routing, SharedModule, NgxDatatableModule, NgbCollapseModule.forRoot(), NgbModule
  ],
  declarations: [TeacherComponent, StudentsComponent, GradeBookComponent, StartWorkComponent],
  providers: [UserService]
})

export class StartWorkModule { }
