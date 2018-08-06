import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { StartWorkComponent } from './start-work/start-work.component';
import { GradeBookComponent } from './start-work/gradebook/gradebook.component';
 
const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: '', component: HomeComponent },
  { path: 'start-work', component: StartWorkComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
