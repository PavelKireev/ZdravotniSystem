import { HttpClient } from "@angular/common/http";
import { Component, Output } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import configurl from '../../assets/config/config.json';
import { AuthService } from "../service/auth.service";

@Component({
  selector: 'myprofile-component',
  templateUrl: './myprofile.component.html',
})
export class MyProfileComponent {

  private readonly baseUrl: string = configurl.apiServer.url + "/api/authentication/"

  @Output()
  public user: AuthUserDto = new AuthUserDto();

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient,
    private actRoute: ActivatedRoute,
    private router: Router
  ) { }

  public ngOnInit() {
    this.getUser();
  }

  getUser(): void {
    this.httpClient.get<AuthUserDto>(this.baseUrl + "my-profile")
                   .subscribe(response => this.user = response);
  }

  public update(user?: AuthUserDto): void {
    
  }

  public isUserAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }

  public isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  public isDoctor(): boolean {
    return this.authService.isDoctor();
  }

  public isPatient(): boolean {
    return this.authService.isPatient();
  }

}

export class AuthUserDto {
  id?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  phoneNumber?: number;
  insuranceNumber?: string;
  birthDate?: Date;
  officeNumber?: number;
}
