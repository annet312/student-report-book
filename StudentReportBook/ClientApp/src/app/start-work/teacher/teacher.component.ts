import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html'
})
export class TeacherComponent {
  public forecasts: Teacher[];
  private baseUrl: string;
  private http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;    
  }

  ngOnInit(){
    this.http.get<Teacher[]>(this.baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
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
