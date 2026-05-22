import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { RegistrationList } from './components/registration-list/registration-list';
import { authGuard } from './guards/auth-guard';
import { EditRegistration } from './components/edit-registration/edit-registration';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'registrations', component: RegistrationList, canActivate: [authGuard] },
  { path: 'edit/:id', component: EditRegistration, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' },
];
