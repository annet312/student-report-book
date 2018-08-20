import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { GradeBookComponent } from './start-work/gradebook/gradebook.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { RoleGuardService as RoleGuard } from './role.guard';
import { TeacherComponent } from './start-work/teacher/teacher.component';
import { ModeratorComponent } from './start-work/moderator/moderator.component';
import { StartWorkComponent } from './start-work/start-work.component';
 
const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '', component: HomeComponent },
  {
    path: 'start-work', component: StartWorkComponent},
  {
    path: 'teacher', component: TeacherComponent, canActivate: [RoleGuard],
    data: {
      expectedRole: 'Teacher'
    }
  },
  {
    path: 'gradebook', component: GradeBookComponent, canActivate: [RoleGuard],
    data: {
      expectedRole: 'Student'
    }
  },
  {
    path: 'moderator', component: ModeratorComponent, canActivate: [RoleGuard],
    data: {
      expectedRole: 'Moderator'
    }
  },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '/404' },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
