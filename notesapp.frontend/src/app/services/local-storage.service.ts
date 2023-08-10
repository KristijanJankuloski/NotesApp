import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public GetJwt() {
    return localStorage.getItem("Token");
  }

  public SetJwt(token: string){
    localStorage.setItem("Token", token);
  }

  public RemoveJwt(){
    localStorage.removeItem("Token");
  }

  public GetCurrentUser(){
    let userString = localStorage.getItem("User");
    if (userString === null){
      return null;
    }
    return JSON.parse(userString);
  }
}
