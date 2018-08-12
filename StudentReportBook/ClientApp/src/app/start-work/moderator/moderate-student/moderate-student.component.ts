import { Component, Inject, ViewChild, ViewChildren, QueryList, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student, Faculty, Group } from '../../../shared/models/gradebook.interface';
import { NgbModal } from '../../../../../node_modules/@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-moderate-student',
  templateUrl: './moderate-student.component.html',
  styleUrls: ['./moderate-student.component.css']
})
export class ModerateStudentComponent implements OnInit {
  private baseUrl: string;
  private students: Student[];
  private faculties: Faculty[];
  private groups: Group[];
  private editing = {};

  @ViewChildren("selectGroup") selGroup: QueryList<any>

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
   // if (event.nextId == "ngb-tab-1") {
      this.http.get<Student[]>(this.baseUrl + 'api/moderator/getStudentsWithoutGroup').subscribe(result => {
        this.students = result;
        if (!this.faculties) {
          this.http.get<Faculty[]>(this.baseUrl + 'api/moderator/getAllFaculties').subscribe(res => {
            this.faculties = res;
          }, error => console.error(error));
        }
      }, error => console.error(error));
   // }
  }

  setFaculty(facultyIndex) {
    this.groups = this.faculties[facultyIndex].groups;
  }

  setGroup(studentId, rowIndex) {
    this.http.get<boolean>(this.baseUrl + 'api/moderator/setGroupForStudent', { params: { studentId: studentId, groupId: this.selGroup.toArray()[rowIndex].nativeElement.value } })
      .subscribe(result => {
        if (result) {
          this.editing[studentId] = !this.editing[studentId];
          this.students.splice(rowIndex, 1);
        }
        else {
          alert("Can't set group");
        }
      }, error => console.error(error));
  }
}
