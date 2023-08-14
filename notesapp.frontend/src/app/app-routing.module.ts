import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotesListComponent } from './notes-list/notes-list.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', component:HomeComponent },
  { path: 'notes', component: NotesListComponent },
  { path: 'login', component: LoginComponent },

  // WILDCARD 404
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
