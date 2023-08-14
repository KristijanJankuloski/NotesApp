import { Component } from '@angular/core';
import ILoginUserModel from '../models/login-user';
import { ApiRequestService } from '../services/api-request.service';
import { LocalStorageService } from '../services/local-storage.service';
import { SessionUserService } from '../services/session-user.service';
import IUser from '../models/user-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router, private api: ApiRequestService, private local: LocalStorageService, private userSession: SessionUserService){}

  loginFormSubmit(logIn:any) {
    let credentials : ILoginUserModel = logIn.form.value;
    try {
      this.api.LogInUser(credentials).subscribe( (response:any) => {
        console.log(response.status);
        this.local.SetJwt(response.token);
        let user : IUser = { username: response.username, email: response.email };
        this.local.SetCurrentUser(user);
        this.userSession.setUser(user);
        this.router.navigate(['/notes']);
      });
    }
    catch(err){
      console.log("cannot connect to server");
    }
  }
}
