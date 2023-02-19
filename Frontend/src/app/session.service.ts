import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class SessionService {
  private role: string = "";
  private id: number = 0;

  get userRole() : string {
    return this.role;
  }

  set userRole(role: string) {
    if (role != "")
      this.role = role;
  }

  get userId() : number {
    return this.id;
  }

  set userId(id: number) {
    if (id >= 0)
      this.id = id;
  }

  constructor() { }
}