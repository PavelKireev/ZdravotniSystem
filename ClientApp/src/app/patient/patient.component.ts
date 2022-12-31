import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Component, Output } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from "rxjs";
import configurl from '../../assets/config/config.json';

@Component({
  selector: 'patient-component',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent {
  title = 'Zdravotni System App';

  @Output()
  public patient: Patient = new Patient();

  private readonly baseUrl: string = configurl.apiServer.url + "/api/patient/"

  constructor(
    private jwtHelper: JwtHelperService,
    private httpClient: HttpClient,
    private actRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getPatient(this.actRoute.snapshot.params['email'])
        .subscribe(response => this.patient = response);
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

  private getPatient(email: string): Observable<Patient> {
    return this.httpClient.get<Patient>(this.baseUrl + "patient?email=" + email);
  }

  private update(patient: Patient): void {
    this.httpClient.post(this.baseUrl + "patient", JSON.stringify(patient), {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe({
      next: (_) => this.router.navigate(["homepage"]),
      //error: (err: HttpErrorResponse) => {
      //  this.errorMessage = err.message;
      //  this.showError = true;
      //}
    });  }
}

export class Patient{
  firstName?: string;
  lastName?: string;
  email?: string;
  phoneNumber?: string;
  insuranseNumber?: string;
  birthDate?: string;
}
