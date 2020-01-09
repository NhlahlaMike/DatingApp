import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('registration successful!');
    },
    error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
