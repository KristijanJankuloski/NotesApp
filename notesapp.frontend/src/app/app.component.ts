import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from './services/local-storage.service';
import { SessionUserService } from './services/session-user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Notes';

  constructor(private router: Router, private localStore: LocalStorageService, private sessionUser: SessionUserService){}

  ngOnInit(){
    let user = this.localStore.GetCurrentUser();
    if (user != null){
      this.sessionUser.setUser(user);
      this.router.navigate(["/notes"]);
    }
  }
}
