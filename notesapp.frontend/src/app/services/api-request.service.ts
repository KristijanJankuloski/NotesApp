import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import IRegisterUserModel from '../models/register-user';
import ILoginUserModel from '../models/login-user';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {

  constructor(private http: HttpClient) {}

  public RegisterUser(model: IRegisterUserModel){
    return this.http.post(`${environment.apiBaseUrl}/auth/register`, model);
  }

  public LogInUser(model: ILoginUserModel){
    return this.http.post(`${environment.apiBaseUrl}/auth/login`, model);
  }
}
