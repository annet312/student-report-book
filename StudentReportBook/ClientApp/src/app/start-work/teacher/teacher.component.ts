import { Component, Inject, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgStyle } from '@angular/common';
import { Student, Mark } from '../../shared/models/gradebook.interface';


@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrls: ['./teacher.component.css']
})
export class TeacherComponent implements OnInit {
  http: HttpClient;
  baseUrl: string;

  public subjects: Dropdata[] = null;
  public faculties: Dropdata[] = null;
  public groups: Dropdata[] = null;
  public students: StudentWithMarks[] = null;
  public terms: number[];
  public subjtId: any;
  public editing = {};

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  ngOnInit() {
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getSubjectsForCurrentTeacher').subscribe(result => {
      this.subjects = result;
    }, error => console.error(error));
  }

  filterSubject(subjectId) {
    this.subjtId = subjectId;
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getFaculties', { params: { subjectId: subjectId } }).subscribe(result => {
      this.faculties = result;
    }, error => console.error(error));
  }

  filterFaculty(facId) {
    this.http.get<Dropdata[]>(this.baseUrl + 'api/teacher/getGroups', { params: { subjectId: this.subjtId, facultyId: facId } }).subscribe(result => {
      this.groups = result;
    }, error => console.error(error));
  }

  filterGroup(groupId) {
    this.http.get<number[]>(this.baseUrl + 'api/teacher/getTerms', { params: { groupId: groupId, subjectId: this.subjtId } })
      .subscribe(result => { this.terms = result });
    this.http.get<StudentWithMarks[]>(this.baseUrl + 'api/teacher/getStudents', { params: { groupId: groupId, subjectId: this.subjtId } })
      .subscribe(result => {
            this.students = result;
    }, error => console.error(error));
  }


  updateValue(event, cell, rowIndex, j) {
    this.editing[rowIndex + '-' + cell + j] = false;
    let body = {
      studentId: this.students[rowIndex].student.id,
      subjectId: this.subjtId,
      term: this.terms[j],
      grade: event.target.value
    };

    this.http.post<Mark>(this.baseUrl + 'api/teacher/editMark', body)
      .subscribe(res => {
        if (res == null) {
          alert("Cannot change grade");
        }
        else {
          this.students[rowIndex].marks[j] = res;
        }
      });
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

