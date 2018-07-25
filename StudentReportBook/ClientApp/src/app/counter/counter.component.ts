import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'  
})
export class CounterComponent {
  public currentCount = 0;
  public students: Student[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Student[]>(baseUrl + 'api/student/getMygradeBook').subscribe(result => {
      this.students = result;
      console.log(result);
    }, error => console.error(error));
  }
  ngOnInit() {

  }

  //public incrementCounter() {
  //  this.currentCount++;
  //}
  
}

interface Student {
  id: number;
  name: string;
  firstName: string;
  lastName: string;
  group: string;
}
