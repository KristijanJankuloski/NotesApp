import { Component } from '@angular/core';
import { SessionUserService } from '../services/session-user.service';
import { LocalStorageService } from '../services/local-storage.service';
import { ApiRequestService } from '../services/api-request.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  username?: string;
  isLoggedIn: boolean = false;

  constructor(private sessionUser: SessionUserService, private local: LocalStorageService, private api: ApiRequestService){}
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
    this.api.logout();
  }
}
