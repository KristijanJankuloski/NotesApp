import { Component } from '@angular/core';
import ILoginUserModel from '../models/login-user';
import { ApiRequestService } from '../services/api-request.service';
import { LocalStorageService } from '../services/local-storage.service';
import { SessionUserService } from '../services/session-user.service';
import IUser from '../models/user-model';
import { Router } from '@angular/router';
import ILogInResponse from '../models/login-response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router, private api: ApiRequestService, private localStorageService: LocalStorageService, private userSession: SessionUserService){}

  loginFormSubmit(logIn:any) {
    let credentials : ILoginUserModel = logIn.form.value;
    this.api.LogInUser(credentials).subscribe({next: (response: ILogInResponse) => {
      console.log(response.token);
      this.localStorageService.SetJwt(response.token);
      let user : IUser = { username: response.username, email: response.email };
      this.localStorageService.SetCurrentUser(user);
      this.userSession.setUser(user);
      this.router.navigate(['/notes']);
    }, error: this.handleError});
  }
  
  private handleResponse(response:ILogInResponse){
  }

  private handleError(error:any) {
    console.log(error);
  }
}
