import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import IRegisterUserModel from '../models/register-user';
import ILoginUserModel from '../models/login-user';
import { LocalStorageService } from './local-storage.service';
import INoteListModel from '../models/note-list-model';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {

  constructor(private http: HttpClient, private local: LocalStorageService) {}

  public RegisterUser(model: IRegisterUserModel){
    return this.http.post(`${environment.apiBaseUrl}/auth/register`, model);
  }

  public LogInUser(model: ILoginUserModel){
    return this.http.post(`${environment.apiBaseUrl}/auth/login`, model);
  }

  public GetUserNotes(){
    const token = this.local.GetJwt();
    if(!token)
      return;
    let headers = new HttpHeaders();
    headers.append("Authorization", `Bearer ${token}`);
    return this.http.get<INoteListModel[]>(`${environment.apiBaseUrl}/notes`, {headers: headers});
  }
}
