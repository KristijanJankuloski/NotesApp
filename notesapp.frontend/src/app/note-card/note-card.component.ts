import { Component, Input } from '@angular/core';
import INoteListModel from '../models/note-list-model';
import colors from '../models/note-color-class';
import { Router } from '@angular/router';

@Component({
  selector: 'app-note-card',
  templateUrl: './note-card.component.html',
  styleUrls: ['./note-card.component.css']
})
export class NoteCardComponent {
  @Input() note: INoteListModel = { id: 0, userId: 0, color: 0, title: "a", text: ""};
  colors: string[] = colors;

  constructor(private router: Router){}
  
  noteClicked() {
    this.router.navigate(['/details', this.note.id])
  }
}
