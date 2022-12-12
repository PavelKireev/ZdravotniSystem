import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvironmentUrlService } from '../shared/services/environment-url.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>()
  public authChanged = this.authChangeSub.asObservable();

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService,
    private jwtHelper: JwtHelperService) { }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public loginUser = (route: string, body: UserForAuthenticationDto) => {
    return this.http.post<AuthResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body).subscribe({
      next: (res: AuthResponseDto) => {
        localStorage.setItem("token", res.token);
        this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
        this.router.navigate([this.returnUrl]);
      }
    });
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return token && !this.jwtHelper.isTokenExpired(token);
  }

  public isUserAdmin = (): boolean => {
    const token: any = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']

    return role === 'Administrator';
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}


export interface UserForRegistrationDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RegistrationResponseDto {
  isSuccessfulRegistration: boolean;
  errros: string[];
}

export interface UserForAuthenticationDto {
  email: string;
  password: string;
}

export interface AuthResponseDto {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
}
