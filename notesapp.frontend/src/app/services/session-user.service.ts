import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import IUser from '../models/user-model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class SessionUserService {

  private user?: IUser;
  private userSubject = new BehaviorSubject<IUser | undefined>(this.user);
  constructor(private local: LocalStorageService) { }

  getUser() : Observable<IUser | undefined>{
    return this.userSubject.asObservable();
  }

  setUser(user: IUser) : void{
    this.user = user;
    this.userSubject.next(this.user);
  }

  removeUser() : void {
    this.user = undefined;
    this.local.DeleteCurrentUser();
    this.local.RemoveJwt();
    this.userSubject.next(this.user);
  }
}
