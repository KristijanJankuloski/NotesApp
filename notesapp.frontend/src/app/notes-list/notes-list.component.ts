import { Component } from '@angular/core';
import { SessionUserService } from '../services/session-user.service';
import { ApiRequestService } from '../services/api-request.service';
import INoteListModel from '../models/note-list-model';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.css']
})
export class NotesListComponent {
  username?: string;
  userNotes: INoteListModel[] = [];
  constructor(private sessionUser: SessionUserService, private api: ApiRequestService){}

  ngOnInit(){
    this.sessionUser.getUser().subscribe(user => {
      if(user){
        this.username = user.username;
        this.getNotes();
      }
      this.userNotes = [];
      this.username = undefined;
    });
  }

  getNotes(){
    this.api.GetUserNotes()?.subscribe(data => {
      console.log(data);
      this.userNotes = data;
    })
  }
}
