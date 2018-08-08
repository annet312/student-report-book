import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { TeacherComponent } from './teacher/teacher.component';
import { ModeratorComponent } from './moderator/moderator.component';
import { GradeBookComponent } from './gradebook/gradebook.component';


export const routing: ModuleWithProviders = RouterModule.forChild([
  { path: 'moderator', component: ModeratorComponent },
  { path: 'teacher', component: TeacherComponent },
  { path: 'gradebook', component: GradeBookComponent }
]);
