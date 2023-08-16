import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import IRegisterUserModel from '../models/register-user';
import ILoginUserModel from '../models/login-user';
import { LocalStorageService } from './local-storage.service';
import INoteListModel from '../models/note-list-model';
import { Observable, catchError, throwError } from 'rxjs';
import ILogInResponse from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {

  constructor(private http: HttpClient, private local: LocalStorageService) {}

  private errorHandler(error: HttpErrorResponse){
    if(error.status === 0)
      console.error("Network error");
    else {
      console.error(`Status code: ${error.status}, Message: ${error.message}`);
    }
    return throwError(() => error);
  }

  public RegisterUser(model: IRegisterUserModel){
    return this.http.post(`${environment.apiBaseUrl}/auth/register`, model).pipe(catchError(this.errorHandler));
  }

  public LogInUser(model: ILoginUserModel){
    return this.http.post<ILogInResponse>(`${environment.apiBaseUrl}/auth/login`, model).pipe(catchError(this.errorHandler));
  }

  public GetUserNotes() : Observable<INoteListModel[]>{
    const token = this.local.GetJwt();
    if(!token)
      throw new Error("No token");
    const headers = {'Authorization': `Bearer ${token}`};
    return this.http.get<INoteListModel[]>(`${environment.apiBaseUrl}/notes`, {headers}).pipe(catchError(this.errorHandler));
  }
}
