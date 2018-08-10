import { Component, Inject, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student } from '../../shared/models/gradebook.interface';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-moderator',
  templateUrl: './moderator.component.html'
})
export class ModeratorComponent {
  private teachers: Teacher[];
  private baseUrl: string;
  private students: Student[];
  private faculties: Faculty[];
  private groups: Group[];
  private editing = {};
  private teacherWs: TeacherWorkload[];

  private closeResult: string;


  @ViewChildren("selectGroup") selGroup: QueryList<any>
  

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(){
    this.http.get<Teacher[]>(this.baseUrl + 'api/moderator/getTeachers').subscribe(result => {
      this.teachers = result;
      console.log(result);
    }, error => console.error(error));   
  }

  getContent(event) {
    if (event.nextId == "ngb-tab-1") {
      this.http.get<Student[]>(this.baseUrl + 'api/moderator/getStudentsWithoutGroup').subscribe(result => {
        this.students = result;
        if (!this.faculties) {
          this.http.get<Faculty[]>(this.baseUrl + 'api/moderator/getAllFaculties').subscribe(res => {
            this.faculties = res;
          }, error => console.error(error));
        }
      }, error => console.error(error));
    }
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
          this.students = [...this.students];
        }
        else {
          alert("Can't set group");
        }
      }, error => console.error(error));
  }

  getTeacherWorkload(teacherId) {
    this.http.get<TeacherWorkload[]>(this.baseUrl + 'api/moderator/getTeacherWorkloads', { params: { teacherId: teacherId}})
      .subscribe(result => {
        this.teacherWs = result;
        console.log(this.teacherWs);
      });
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


interface Teacher {
  id: number;
  name: string;
  firstName: string;
  lastName: string;
  department: string;
}

interface Faculty {
  id: number;
  name: string;
  groups: Group[];
}

interface Group {
  id: number;
  name: string;
}

interface TeacherWorkload {
  id: number;
  group: Group;
  term: number;
  subject: Subject;
}

interface Subject{
  id: number;
  name: string;
}
