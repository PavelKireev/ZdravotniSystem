import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, shareReplay, switchMap } from 'rxjs';
import configurl from '../../assets/config/config.json'
import { WorkingHours } from '../working-hours/working-hours.component';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class WorkingHoursService {

  private readonly baseUrl: string = configurl.apiServer.url + "/api/working-hours/"

  constructor(
    private authService: AuthService,
    private httpClient: HttpClient,
  ) { }

  public saveWorkingHours(workingHours: WorkingHours, day: string): Observable<WorkingHours[]> {
    workingHours.dayOfWeek = day;
    return this.httpClient.post<WorkingHours[]>(this.baseUrl + "create?doctorId=" + this.authService.getAuthUserId(), JSON.stringify(workingHours), {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).pipe(shareReplay());
  }

  public remove(id?: number): Observable<WorkingHours[]> {
    return this.httpClient.delete<WorkingHours[]>(
      configurl.apiServer.url + "/api/working-hours/delete?id=" + id + "&doctorId=" + this.authService.getAuthUserId()).pipe(shareReplay());
  }

  public getAllByDoctorId(): Observable<WorkingHours[]> {
    return this.httpClient.get<WorkingHours[]>(this.baseUrl + 'list?doctorId=' + this.authService.getAuthUserId());
  }
}
