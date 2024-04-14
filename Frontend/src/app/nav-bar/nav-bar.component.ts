import { Component, OnInit } from '@angular/core';
import { AlertyfyService } from '../services/alertyfy.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
  loggedinUser: string | null | undefined;
  
  constructor(private alertify : AlertyfyService) {}

  ngOnInit() {}
  loggedin() {
    this.loggedinUser = localStorage.getItem('userName');
    return this.loggedinUser;
  }
  onLogout(){
    localStorage.removeItem('userName');
    localStorage.removeItem('token');
    this.alertify.success("You are logged out");
  }
}
