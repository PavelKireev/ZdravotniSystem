import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './guards/auth-guard.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { DoctorComponent } from './doctor/doctor.component';
import { PatientComponent } from './patient/patient.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';

const routes: Routes = [
  { path: '', component: HomepageComponent },
  { path: 'appointment', component: AppointmentComponent, canActivate: [AuthGuard] },
  { path: 'doctor', component: DoctorComponent, canActivate: [AuthGuard] },
  { path: 'patient', component: PatientComponent, canActivate: [AuthGuard] },
  { path: 'registration', component: RegisterUserComponent }
];

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    AppointmentComponent,
    DoctorComponent,
    HomepageComponent,
    LoginComponent,
    NavMenuComponent,
    PatientComponent,
    RegisterUserComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7299"],
        disallowedRoutes: []
      }
    }),
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
