import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GradeBook } from '../shared/models/gradebook.interface';



@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'  
})
export class CounterComponent {
  public currentCount = 0;
  public gradebook: GradeBook;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<GradeBook>(baseUrl + 'api/student/getMygradeBook').subscribe(result => {
      this.gradebook = result;
      console.log(result);
    }, error => console.error(error));
  }
  ngOnInit() {

  }

  
}

