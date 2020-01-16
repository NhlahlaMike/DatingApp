import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
  // '' instead of home to redirect to home
  { path: '', component: HomeComponent},
  { // protecting multiple routes
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard], /* authorize all childres routes*/
    children: [
      { path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver}/*, canActivate: [AuthGuard]*/},
      { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}},
      {path: 'member/edit',  component: MemberEditComponent, resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
      { path: 'messages', component: HomeComponent},
      { path: 'lists', component: HomeComponent},
    ]
  },
  // for unspecified routes
  { path: '**', redirectTo: '', pathMatch: 'full'},
];

// Pass id: members/:id Or member/edit : it is retricted to use members/edit "s" must be removed
// since it /:id passes id as well
