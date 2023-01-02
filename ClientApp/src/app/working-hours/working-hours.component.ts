import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import configurl from '../../assets/config/config.json';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'working-hours',
  templateUrl: './working-hours.component.html',
})
export class WorkingHoursComponent {

  private doctorId?: number;

  @Output()
  public workingHours: WorkingHours = new WorkingHours();
  @Output()
  public list: Array<WorkingHours> = [];
  @Output()
  public days: string[] = [];

  @Output()
  public selectedDay: string = '';

  private readonly baseUrl: string = configurl.apiServer.url + "/api/working-hours/"

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient,
    private actRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.doctorId = this.authService.getAuthUserId();
    this.getAllByDoctorId(this.doctorId);
    this.workingHours.doctorId = this.doctorId;
  }

  public getAllByDoctorId(doctorId: number): void {
    this.doctorId = doctorId;
    this.httpClient.get<WorkingHours[]>(this.baseUrl + 'list?doctorId=' + doctorId)
      .subscribe(response => {
        this.list = response;
        this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !this.list.some(wh => wh.dayOfWeek === day));
      });

  }

  public create(workingHours: WorkingHours, day: string): void {
    workingHours.dayOfWeek = day;
    this.httpClient.post<WorkingHours[]>(this.baseUrl + "create?doctorId=" + this.authService.getAuthUserId(), JSON.stringify(workingHours), {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(
      response => {
        this.list = response;
        this.workingHours = new WorkingHours();
        this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !this.list.some(wh => wh.dayOfWeek === day));
      }
    );
  }

  public delete(id?: number): void {
    this.httpClient.delete<WorkingHours[]>(
      configurl.apiServer.url + "/api/working-hours/delete?id=" + id + "&doctorId=" + this.authService.getAuthUserId()
    ).subscribe(
      response => {
        this.list = response;
        this.workingHours = new WorkingHours();
        this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !this.list.some(wh => wh.dayOfWeek === day));
      }
    );
  }

  public isUserAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }

}

export class WorkingHours {
  id?: number;
  dayOfWeek: string = '';
  hourFrom?: number;
  hoursCount?: number;
  doctorId?: number;
}
