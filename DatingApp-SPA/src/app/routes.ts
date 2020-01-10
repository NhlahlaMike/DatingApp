import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
  // '' instead of home to redirect to home
  { path: '', component: HomeComponent},
  { // protecting multiple routes
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'members', component: HomeComponent, canActivate: [AuthGuard]},
      { path: 'messages', component: HomeComponent},
      { path: 'lists', component: HomeComponent},
    ]
  },
  // for unspecified routes
  { path: '**', redirectTo: '', pathMatch: 'full'},
];
