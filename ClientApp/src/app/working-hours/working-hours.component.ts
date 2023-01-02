import { Component } from '@angular/core';

@Component({
  selector: 'working-hours',
  templateUrl: './working-hours.component.html',
})
export class WorkingHoursComponent {

  public workingHours: WorkingHours = new WorkingHours();


}

export class WorkingHours {
  id?: number;
  dayOfWeek?: string;
  hourFrom?: number;
  hoursCount?: number;
  doctorId?: number;
}
