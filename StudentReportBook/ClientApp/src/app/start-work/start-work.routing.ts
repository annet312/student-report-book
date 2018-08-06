import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { TeacherComponent } from './teacher/teacher.component';
import { StudentsComponent } from './students/students.component';
import { GradeBookComponent } from './gradebook/gradebook.component';


export const routing: ModuleWithProviders = RouterModule.forChild([
  { path: 'teacher', component: TeacherComponent },
  { path: 'student', component: StudentsComponent },
  { path: 'gradebook', component: GradeBookComponent }
]);
