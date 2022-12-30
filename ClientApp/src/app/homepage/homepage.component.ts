import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { mergeMap } from 'rxjs';

@Component({
  selector: 'hompage-component',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {

  public tableList?: User[];

  constructor(
    private jwtHelper: JwtHelperService,
    private httpClient: HttpClient,
    private router: Router
  ) { }

  isUserAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  fillTable(): void {
    this.httpClient.get<User[]>("aw").subscribe(
      (users: User[]) => this.tableList = users
    );
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
  }
}

export interface User {
  FirstName: string;
  LastName: string;
}
