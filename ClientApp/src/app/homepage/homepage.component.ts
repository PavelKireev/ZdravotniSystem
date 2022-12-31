import { HttpClient } from '@angular/common/http';
import { Component, Output } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import configurl from '../../assets/config/config.json';

@Component({
  selector: 'hompage-component',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {

  @Output()
  public tableList?: User[];

  constructor(
    private jwtHelper: JwtHelperService,
    private httpClient: HttpClient,
    private router: Router
  ) { }

  ngOnInit(): void{
    this.fillTable();
  }

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
    this.httpClient.get<User[]>(configurl.apiServer.url + "/api/patient").subscribe(
      (users: User[]) => {
        this.tableList = users;
        users.forEach(
          (u) => console.log(u.firstName + " " + u.lastName)
        );
      }
    );
    console.log(this.tableList?.toString());
  }

  public moreInfo($event: any, email: string, role: string): void {
    switch (role) {
      case "PATIENT":
        this.router.navigate(["patient", { email: email }])
        break;
      case "DOCTOR":
        this.router.navigate(["doctor", email])
        break;
      case "ADMIN":
        this.router.navigate(["admin", email])
        break;
    }
  }

  public delete($event: any, email: string): void {
    this.httpClient.delete(
      configurl.apiServer.url + "/api/patient/delete?email=" + email
    ).subscribe(
      _ => this.tableList = this.tableList?.filter(user => user.email !== email),
      error => console.log("User delete error.")
    );
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
  }
}

export interface User {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
}
