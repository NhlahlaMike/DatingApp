import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {

  }
  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Registration Successful');
    },
    error => {
      this.alertify.error(error);
    });
  }

  loggedin() {
    return this.authService.loggedIn();
    /*const token = localStorage.getItem('token');
    return !!token;  // !! return true or  false value: if empty =false, if not empty =true*/
  }

  logout() {
    localStorage.removeItem('token');
  }
}
