import { Component, Inject } from '@angular/core';
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

  getStudents() {
    this.http.get<Student[]>(this.baseUrl + 'api/moderator/getStudents').subscribe(result => {
      this.students = result;
      console.log(result);
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
