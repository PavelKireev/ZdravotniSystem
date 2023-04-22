import { Router } from '@angular/router';
import { PasswordConfirmationValidatorService } from './../../shared/custom-validators/password-confirmation-validator.service';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import configurl from '../../../assets/config/config.json';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  registerForm!: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;

  constructor(
    private router: Router,
    private passConfValidator: PasswordConfirmationValidatorService,
    private httpClient: HttpClient
  ) { }

  url = configurl.apiServer.url + '/api/authentication/';

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirm: new FormControl('')
    });
    this.registerForm.get('confirm')?.setValidators([Validators.required,this.passConfValidator.validateConfirmPassword(this.registerForm?.get('password'))]);
  }

  public validateControl = (controlName: string) => {
    return this.registerForm?.get(controlName)?.invalid && this.registerForm?.get(controlName)?.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm?.get(controlName)?.hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.showError = false;
    const formValues = { ...registerFormValue };

    const user: UserForRegistrationDto = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm
    };

    this.httpClient.post(this.url + "sign-up", JSON.stringify(user), {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe({
      next: (_) => this.router.navigate(["login"]),
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
      }
    });
  }
}

interface UserForRegistrationDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string
}
