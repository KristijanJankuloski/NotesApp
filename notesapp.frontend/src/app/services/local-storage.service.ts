import { Injectable } from '@angular/core';
import IUser from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public GetJwt() : string | null {
    return localStorage.getItem("Token");
  }

  public GetRefreshToken() : string | null {
    return localStorage.getItem("RefreshToken");
  }

  public SetJwt(token: string, refresh: string) : void {
    localStorage.setItem("Token", token);
    localStorage.setItem("RefreshToken", refresh);
  }

  public RemoveJwt() : void{
    localStorage.removeItem("Token");
    localStorage.removeItem("RefreshToken");
  }

  public GetCurrentUser() : IUser | null{
    let userString = localStorage.getItem("User");
    if (userString === null){
      return null;
    }
    return JSON.parse(userString);
  }

  public SetCurrentUser(user: IUser) : void {
    let userString = JSON.stringify(user);
    localStorage.setItem("User", userString);
  }

  public DeleteCurrentUser() : void {
    localStorage.removeItem("User");
  }
}
