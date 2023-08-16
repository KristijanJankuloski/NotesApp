import { Component, Input } from '@angular/core';
import INoteListModel from '../models/note-list-model';

@Component({
  selector: 'app-note-card',
  templateUrl: './note-card.component.html',
  styleUrls: ['./note-card.component.css']
})
export class NoteCardComponent {
  @Input() note: INoteListModel = { id: 0, userId: 0, color: 0, title: "a", text: ""};
  colors: string[] = ["card-red", "card-green", "card-blue", "card-yellow", "card-purple", "card-orange"];
}
