import { Component } from '@angular/core';
import { SessionUserService } from '../services/session-user.service';
import { LocalStorageService } from '../services/local-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  username?: string;
  isLoggedIn: boolean = false;

  constructor(private sessionUser: SessionUserService, private local: LocalStorageService){}
  ngOnInit() {
    this.sessionUser.getUser().subscribe((user) => {
      if (!user){
        this.isLoggedIn = false;
        return;
      }
      this.username = user?.username;
      this.isLoggedIn = true;
    });
  }

  logOutClick() {
    this.local.DeleteCurrentUser();
    this.sessionUser.removeUser();
    this.local.RemoveJwt();
  }
}
