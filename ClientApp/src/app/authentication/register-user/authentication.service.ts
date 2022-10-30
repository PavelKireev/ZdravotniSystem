import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";



@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) { }

  public registerUser = (route: string, body: UserForRegistrationDto) => {
    return this.http.post<RegistrationResponseDto>(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
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