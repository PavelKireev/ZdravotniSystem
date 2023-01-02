import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private jwtHelper: JwtHelperService
  ) { }

  public isUserAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  public logout = () => {
    localStorage.removeItem("jwt");
  }

  public isAdmin(): boolean {
    return this.getRole() === 'ADMIN';
  }

  public isDoctor(): boolean {
    return this.getRole() === 'DOCTOR';
  }

  public isPatient(): boolean {
    return this.getRole() === 'PATIENT';
  }

  public getAuthUserEmail(): string {
    let token = localStorage.getItem("jwt");
    if (token !== null) {
      return this.jwtHelper.decodeToken(token)["email"];
    } else {
      return '';
    }
  }

  public getAuthUserId(): number {
    let token = localStorage.getItem("jwt");
    if (token !== null) {
      return this.jwtHelper.decodeToken(token)["id"];
    } else {
      return 0;
    }
  }

  private getRole(): string {
    let token = localStorage.getItem("jwt");
    if (token !== null) {
      return this.jwtHelper.decodeToken(token)["role"];
    } else {
      return '';
    }
  }
}
