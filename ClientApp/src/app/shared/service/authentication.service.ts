import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private _authChangeSub = new Subject<boolean>()
  public authChanged = this._authChangeSub.asObservable();

  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService, private jwtHelper: JwtHelperService) { }


  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return token && !this.jwtHelper.isTokenExpired(token);
  }
  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
