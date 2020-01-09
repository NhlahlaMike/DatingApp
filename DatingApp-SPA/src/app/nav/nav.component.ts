import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {

  }
  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully')
    },
    error => {
      console.log(error);
    });
  }

  loggedin() {
    const token = localStorage.getItem('token');
    return !!token;  // !! return true or  false value: if empty =false, if not empty =true
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
