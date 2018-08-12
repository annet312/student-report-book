import { Component, Inject, ViewChild, ViewChildren, QueryList, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student, Faculty, Group, Teacher, TeacherWorkload, Subject } from '../../../shared/models/gradebook.interface';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { forEach } from '../../../../../node_modules/@angular/router/src/utils/collection';

@Component({
  selector: 'app-moderate-teacher',
  templateUrl: './moderate-teacher.component.html',
  styleUrls: ['./moderate-teacher.component.css']
})
export class ModerateTeacherComponent implements OnInit {
  private teachers: Teacher[];
  private baseUrl: string;
  private listOfGroups: Group[];
  private editing = {};
  private teacherWs: TeacherWorkload[];
  private subjects: Subject[];

  private closeResult: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<Teacher[]>(this.baseUrl + 'api/moderator/getTeachers').subscribe(result => {
      this.teachers = result;
    }, error => console.error(error));   
  }

  getTeacherWorkload(teacherId) {
    this.http.get<TeacherWorkload[]>(this.baseUrl + 'api/moderator/getTeacherWorkloads', { params: { teacherId: teacherId } })
      .subscribe(result => {
        this.teacherWs = result;
        console.log(this.teacherWs);
      });
  }

  getAllSubjects() {
    this.http.get<Subject[]>(this.baseUrl + 'api/moderator/getAllSubjects').subscribe(result => {
      this.subjects = result;
      console.log(result);
    }, error => console.error(error)); 
  }

  updateSubject(event, teacherWorkloadId, rowIndex) {
    this.http.get<boolean>(this.baseUrl + 'api/moderator/changeSubject', { params: { teacherWorkloadId: teacherWorkloadId, subjectId: event.target.value } })
      .subscribe(result => {
        if (result) {
          this.editing[rowIndex + '-subject'] = false;
          this.teacherWs[rowIndex].subject = {
            id: event.target.value,
            name: event.target.text
          };
        }
        else {
          alert("Can't set subject");
        }
      }, error => console.error(error));
  }

  updateTerm(event, teacherWorkloadId, rowIndex) {
    console.log(event);
    this.http.get<boolean>(this.baseUrl + 'api/moderator/changeTerm', { params: { teacherWorkloadId: teacherWorkloadId, term: event.target.value } })
      .subscribe(result => {
        if (result) {
          this.editing[rowIndex + '-term'] = false;
          this.teacherWs[rowIndex].term = event.target.value;
        }
        else {
          alert("Can't set term");
        }
      }, error => console.error(error));
  }

  updateGroup(event, teacherWorkloadId, rowIndex) {
    console.log(event);
    this.http.get<boolean>(this.baseUrl + 'api/moderator/changeGroup', { params: { teacherWorkloadId: teacherWorkloadId, groupId: event.target.value } })
      .subscribe(result => {
        if (result) {
          this.editing[rowIndex + '-group'] = false;
          this.teacherWs[rowIndex].group = {
            id: event.target.value,
            name: event.target.text
          };

        }
        else {
          alert("Can't set group");
        }
      }, error => console.error(error));
  }

  getAllGroups() {
    if (!this.listOfGroups) {
      this.http.get<Group[]>(this.baseUrl + 'api/moderator/getAllGroups').subscribe(res => {
        this.listOfGroups = res;
      }, error => console.error(error));
    }
    console.log(this.listOfGroups);
  }

  open(content, teacherId) {
    this.getTeacherWorkload(teacherId);
    this.modalService.open(content).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
