import { Component, Inject, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgStyle } from '@angular/common';
import { Student, Mark } from '../../shared/models/gradebook.interface';


@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  http: HttpClient;
  baseUrl: string;

  public subjects: Dropdata[] = null;
  public faculties: Dropdata[] = null;
  public groups: Dropdata[] = null;
  public students: StudentWithMarks[] = null;
  public terms: number[];
  public subjtId: any;
 
  

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getSubjectsForCurrentTeacher').subscribe(result => {
      this.subjects = result;
      console.log(result);
    }, error => console.error(error));
  }

  filterSubject(subjectId) {
    this.subjtId = subjectId;
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getFaculties', { params: { subjectId: subjectId } }).subscribe(result => {
      this.faculties = result;
      console.log(result);
    }, error => console.error(error));
  }

  filterFaculty(facId) {
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getGroups', { params: { subjectId: this.subjtId, facultyId: facId } }).subscribe(result => {
      this.groups = result;
      console.log(result);
    }, error => console.error(error));
  }

  filterGroup(groupId) {
    this.http.get<StudentWithMarks[]>(this.baseUrl + 'api/teacher/getStudents', { params: { groupId: groupId, subjectId: this.subjtId } }).subscribe(result => {
      this.students = result;
      console.log(this.students);
      if (!!this.students) {
        let buf: number[] = new Array(this.students[0].student.currentTerm);
        for (let i = 1; i <= this.students[0].student.currentTerm; i++) {
          buf[i-1] = i;
        }
        this.terms = buf;
      }
    }, error => console.error(error));
  }
}

interface Dropdata {
  id: number;
  name: string;
}
interface StudentWithMarks {
  student: Student;
  marks: Mark[];
}

