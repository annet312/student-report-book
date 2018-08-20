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
  private errorString = {};

  @ViewChildren("selectGroup") selGroup: QueryList<any>
  @ViewChildren("studentCardInput") studentCard: QueryList<any>

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
      this.http.get<Student[]>(this.baseUrl + 'api/moderatorStudent/getStudentsWithoutGroup').subscribe(result => {
        this.students = result;
        if (!this.faculties) {
          this.http.get<Faculty[]>(this.baseUrl + 'api/moderatorStudent/getAllFaculties').subscribe(res => {
            this.faculties = res;
          }, error => console.error(error));
        }
      }, error => console.error(error));
  }

  setFaculty(facultyIndex) {
    this.groups = this.faculties[facultyIndex].groups;
  }
  checkStudentCard(event, studentId) {
    console.log(event.target.value)
    if ((event.target.value > 10000) && (event.target.value < 100000)) {
      this.editing[studentId + '-studentcard'] = true;
      this.errorString[studentId] = null;
    }
    else {
      this.errorString[studentId] = 'Student card must be in range from 10000 to 99999';
    }
  }

  setGroup(studentId, rowIndex) {

    let stCard = this.studentCard.toArray()[rowIndex].nativeElement.value;
    this.http.get<boolean>(this.baseUrl + 'api/moderatorStudent/setGroupForStudent', { params: { studentId: studentId, groupId: this.selGroup.toArray()[rowIndex].nativeElement.value,studentCard: stCard} })
      .subscribe(result => {
        if (result) {
          this.students.splice(rowIndex, 1);
        }
        else {
          alert("Can't set group or student card");
        }
      }, error => {
        console.error(error.error);
        alert("Can't set group or student card");
      });
  }
}
