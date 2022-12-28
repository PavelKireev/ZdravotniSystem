import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isUserAuthenticated: boolean;

  constructor(
    //private authService: AuthenticationService,
    private router: Router
  ) {
    this.isUserAuthenticated = false;
  }

  ngOnInit(): void {
    //this.authService.authChanged
    //  .subscribe(res => {
    //    this.isUserAuthenticated = res;
    //  })
  }

  public logout = () => {
    //this.authService.logout();
    //this.router.navigate(["/"]);
  }

  public patientsTable($event: any) {
    console.log("clicked", $event);
  }

  public appointmentsTable($event: any) {

  }
}
