import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }

  ngOnInit(): void {

  }

  public showTable($event: any, type: string) {
    this.router.navigate(["list", { type: type }]);
  }

  public isUserAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }

  public isAdmin(): boolean {
    return this.isUserAuthenticated() && this.authService.isAdmin();
  }

  public isDoctor(): boolean {
    return this.isUserAuthenticated() && this.authService.isDoctor();
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/list"]);
  }

}
