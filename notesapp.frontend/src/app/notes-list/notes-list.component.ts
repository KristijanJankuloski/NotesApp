import { Component, ChangeDetectorRef } from '@angular/core';
import { SessionUserService } from '../services/session-user.service';
import { ApiRequestService } from '../services/api-request.service';
import INoteListModel from '../models/note-list-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.css']
})
export class NotesListComponent {
  username?: string;
  userNotes: INoteListModel[] = [];
  constructor(private sessionUser: SessionUserService, private api: ApiRequestService, private cdr: ChangeDetectorRef, private router: Router){}

  ngOnInit(){
    this.sessionUser.getUser().subscribe((user) => {
      if(user){
        this.username = user.username;
        this.getNotes();
      }
      else {
        this.userNotes = [];
        this.username = undefined;
      }
    });
  }

  getNotes(){
    this.api.GetUserNotes()?.subscribe({next: this.handleResponse, error: this.handleError});
  }
  
  handleResponse = (data:INoteListModel[]) => {
    this.userNotes = [...data];
    this.cdr.detectChanges();
  }

  handleError = (error:any) => {
    if(error.status === 401){
      console.log("Token expired");
      this.api.RefreshToken();
      this.getNotes();
    }
    console.log(error);
  }
}
