import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { StartWorkComponent } from './start-work/start-work.component';
import { GradeBookComponent } from './start-work/gradebook/gradebook.component';
import { NotFoundComponent } from './not-found/not-found.component';
 
const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '', component: HomeComponent },
  { path: 'start-work', component: StartWorkComponent },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '/404' },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
