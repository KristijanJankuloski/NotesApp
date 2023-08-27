import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import IRegisterUserModel from '../models/register-user';
import ILoginUserModel from '../models/login-user';
import { LocalStorageService } from './local-storage.service';
import INoteListModel from '../models/note-list-model';
import { Observable, catchError, throwError } from 'rxjs';
import ILogInResponse from '../models/login-response';
import { Router } from '@angular/router';
import { SessionUserService } from './session-user.service';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {

  constructor(private http: HttpClient, private local: LocalStorageService, private router: Router, private sessionUser: SessionUserService) {}

  private errorHandler(error: HttpErrorResponse){
    if(error.status === 0)
      console.error("Network error");
    else {
      console.error(`Status code: ${error.status}, Message: ${error.message}`);
    }
    return throwError(() => error);
  }

  private refreshErrorHandler(error: HttpErrorResponse) {
    if(error.status === 401){
      this.logout();
    }
    return throwError(() => error);
  }

  public logout() : void {
    this.local.DeleteCurrentUser();
    this.local.RemoveJwt();
    this.sessionUser.removeUser();
    this.router.navigate(['login']);
  }

  get isLoggedIn() {
    return !!this.local.GetJwt();
  }

  public RegisterUser(model: IRegisterUserModel) {
    return this.http.post(`${environment.apiBaseUrl}/auth/register`, model).pipe(catchError(this.errorHandler));
  }

  public LogInUser(model: ILoginUserModel) : Observable<ILogInResponse> {
    return this.http.post<ILogInResponse>(`${environment.apiBaseUrl}/auth/login`, model).pipe(catchError(this.errorHandler));
  }

  public RefreshToken() : Observable<ILogInResponse> {
    const refresh = this.local.GetRefreshToken();
    const token = this.local.GetJwt();
    if(!refresh)
      throw new Error("No refresh token");
    const headers = {'Authorization': `Bearer ${refresh}`};
    let response = this.http.post<ILogInResponse>(`${environment.apiBaseUrl}/auth/token-refresh`, {lastToken: token}, {headers}).pipe(catchError(this.refreshErrorHandler));
    response.subscribe(data => {
      console.log("Token refreshed");
      this.local.SetJwt(data.token, data.refreshToken);
    });
    return response;
  }

  public GetUserNotes() : Observable<INoteListModel[]>{
    const token = this.local.GetJwt();
    console.log("Getting notes")
    if(!token)
      throw new Error("No token");
    const headers = {'Authorization': `Bearer ${token}`};
    let response = this.http.get<INoteListModel[]>(`${environment.apiBaseUrl}/notes`, {headers}).pipe(catchError(this.errorHandler));
    return response;
  }
}
