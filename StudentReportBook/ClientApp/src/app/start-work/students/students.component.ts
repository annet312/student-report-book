import { Component, Inject, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgStyle } from '@angular/common';
//import { Subject } from '../../../node_modules/rxjs';


@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  http: HttpClient;
  baseUrl: string;

  public subjects: StSubject[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    this.http.get<StSubject[]>(this.baseUrl + 'api/teacher/getSubjectsForCurrentTeacher').subscribe(result => {
      this.subjects = result;
      console.log(result);

    }, error => console.error(error));
  }
}

interface Faculty {
  id: number;
  name: string;
}

interface StSubject {
  id: number;
  name: string;
}

