import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AlertyfyService } from '../../services/alertyfy.service';
import { Router } from '@angular/router';
import { UserForLogin } from '../../model/user';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css'],
})
export class UserLoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private alertyfy: AlertyfyService,
    private router: Router
  ) {}

  ngOnInit() {}

  onLogin(loginForm: NgForm) {
    console.log(loginForm.value);
    const token = this.authService.authUser(loginForm.value).subscribe(
      (response: UserForLogin | any) => {
        console.log(response);
        const user = response;
        localStorage.setItem('token', user.token);
        localStorage.setItem('userName', user.userName);
        this.alertyfy.success('Login Successful');
        this.router.navigate(['/']);
      },
      (error) => {
        this.alertyfy.error(error.error);
      }
    );
   
  }
}
