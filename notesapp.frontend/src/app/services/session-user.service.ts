import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import IUser from '../models/user-model';

@Injectable({
  providedIn: 'root'
})
export class SessionUserService {

  private user?: IUser;
  private userSubject = new BehaviorSubject<IUser | undefined>(this.user);
  constructor() { }

  getUser() : Observable<IUser | undefined>{
    return this.userSubject.asObservable();
  }

  setUser(user: IUser) : void{
    this.user = user;
    this.userSubject.next(this.user);
  }

  removeUser() : void {
    this.user = undefined;
    this.userSubject.next(this.user);
  }
}
