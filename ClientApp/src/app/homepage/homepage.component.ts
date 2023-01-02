import { HttpClient } from '@angular/common/http';
import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  public currentType?: string;

  constructor(
    private actRoute: ActivatedRoute,
    private jwtHelper: JwtHelperService,
    private httpClient: HttpClient,
    private router: Router,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {
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

  isAdmin(): boolean {
    return true;
  }

  fillTable(): void {
    let type = this.actRoute.snapshot.params['type']
    this.tableList = [];
    this.httpClient.get<User[]>(configurl.apiServer.url + "/api/" + type + "/list").subscribe(
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
        this.router.navigate(["doctor", { email: email }])
        break;
      case "ADMIN":
        this.router.navigate(["admin", { email: email }])
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
}

export interface User {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
}
