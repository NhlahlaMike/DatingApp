import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
values: any;
private readonly baseUrl = 'https://localhost:44362/api/values';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    this.http.get(this.baseUrl + '/GetValues').subscribe(res => {
      this.values = res;
    },
    error => {
      console.log(error);
    });
  } 
}
