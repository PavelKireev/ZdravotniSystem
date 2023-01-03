import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, switchMap } from 'rxjs';
import configurl from '../../assets/config/config.json';
import { AuthService } from '../service/auth.service';
import { WorkingHoursService } from '../service/working-hours.service';

@Component({
  selector: 'working-hours',
  templateUrl: './working-hours.component.html',
})
export class WorkingHoursComponent {

  public idToDelete?: number;

  @Output()
  public workingHours: WorkingHours = new WorkingHours();
  @Output()
  public list: Observable<WorkingHours[]> = of();
  public removeObservable: Observable<WorkingHours[]> = of();
  public createObservable: Observable<WorkingHours[]> = of();

  @Output()
  public days: string[] = [];

  @Output()
  public selectedDay: string = '';

  private readonly baseUrl: string = configurl.apiServer.url + "/api/working-hours/"

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient,
    private workingHoursService: WorkingHoursService
  ) { }

  ngOnInit(): void {
    this.list = this.workingHoursService.getAllByDoctorId();
    this.list.subscribe(response => {
      this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !response.some(wh => wh.dayOfWeek === day));
    });
  }

  public create(workingHours: WorkingHours, day: string): void {
    this.list = this.workingHoursService.saveWorkingHours(workingHours, day);
    this.list.subscribe(response => {
      this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !response.some(wh => wh.dayOfWeek === day));
    });
  }

  public delete(id?: number): void {
    this.list = this.workingHoursService.remove(id);
    this.list.subscribe(response => {
      this.days = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"].filter(day => !response.some(wh => wh.dayOfWeek === day));
    });
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
