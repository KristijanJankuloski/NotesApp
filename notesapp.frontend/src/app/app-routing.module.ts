import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotesListComponent } from './notes-list/notes-list.component';

const routes: Routes = [
  { path: '', component:HomeComponent },
  { path: 'notes', component: NotesListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
