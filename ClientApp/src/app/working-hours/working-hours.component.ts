import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import configurl from '../../assets/config/config.json';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'working-hours',
  templateUrl: './working-hours.component.html',
})
export class WorkingHoursComponent {

  public workingHours: WorkingHours = new WorkingHours();
  public list: Array<WorkingHours> = [];
  public days: string[] = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"]

  private readonly baseUrl: string = configurl.apiServer.url + "/api/working-hours/"

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient,
    private actRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getAllByDoctorId(this.actRoute.snapshot.params['doctorId']);
  }

  public getAllByDoctorId(doctorId: number): void {
    this.httpClient.get<WorkingHours[]>(this.baseUrl + 'list')
                   .subscribe(response => this.list = response);
  }

  public create(workingHours: WorkingHours): void {
    this.httpClient.post(this.baseUrl + "create", JSON.stringify(workingHours), {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe({
      next: (_) => this.router.navigate(["homepage"]),
      //error: (err: HttpErrorResponse) => {
      //  this.errorMessage = err.message;
      //  this.showError = true;
      //}
    });
  }

  public delete(id: number): void {
    this.httpClient.delete(
      configurl.apiServer.url + "/api/patient/delete?email=" + id
    ).subscribe(
      _ => this.list = this.list?.filter(element => element.id !== element.id),
    );
  }

  public isUserAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }
}

export class WorkingHours {
  id?: number;
  dayOfWeek?: string;
  hourFrom?: number;
  hoursCount?: number;
  doctorId?: number;
}
