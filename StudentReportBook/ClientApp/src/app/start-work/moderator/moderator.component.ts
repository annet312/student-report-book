import { Component, Inject, ViewChild, ViewChildren, QueryList } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student } from '../../shared/models/gradebook.interface';

@Component({
  selector: 'app-moderator',
  templateUrl: './moderator.component.html'
})
export class ModeratorComponent {
  public teachers: Teacher[];
  private baseUrl: string;
  private http: HttpClient;
  public students: Student[];
  public faculties: Faculty[];
  public groups: Group[];
  public editing = {};

  @ViewChildren("selectGroup") selGroup: QueryList<any>
  

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;    
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
    console.log(facultyIndex);
    this.groups = this.faculties[facultyIndex].groups;
    console.log(this.faculties);
  }

  setGroup(studentId, rowIndex) {
    console.log(studentId);
    console.log(rowIndex);
    console.log(this.selGroup.toArray()[rowIndex].nativeElement.value);
    this.http.get<boolean>(this.baseUrl + 'api/moderator/setGroupForStudent', { params: { studentId: studentId, groupId: this.selGroup.toArray()[rowIndex].nativeElement.value } })
      .subscribe(result => {
        console.log(result)
      }, error => console.error(error));
    
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
