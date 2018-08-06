import { Component, Inject, ViewEncapsulation, ViewChild } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { GradeBook, Student, Mark } from '../../shared/models/gradebook.interface';
import { NgStyle } from '@angular/common';
import { DatatableComponent } from '@swimlane/ngx-datatable';



@Component({
  selector: 'app-gradebook',
  templateUrl: './gradebook.component.html'  
})

export class GradeBookComponent {
  public gradebook: GradeBook;

  @ViewChild('markTable') markTable: any;

  row = [];
  rowmarks = [];
  groups = [];
  http: HttpClient;
  baseUrl: string;
  

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  ngOnInit() {

    var token = localStorage.getItem('auth_token');
    
    this.http.get<GradeBook>(this.baseUrl + 'api/student/getMyGradeBook' ).subscribe(result => {
      this.gradebook = result;
      console.log(result);

      this.row = [{
        name: this.gradebook.student.name,
        studentCard: this.gradebook.student.studentCard,
        currentTerm: this.gradebook.student.currentTerm,
        group: this.gradebook.student.group,
        faculty: this.gradebook.student.faculty
      }];
      this.rowmarks = this.gradebook.marks;

    }, error => console.error(error));
  }

  getGroupRowHeight(group, rowHeight) {
    let style = {};
    style = {
      height: (group.length * 40) + 'px',
      width: '100%'
    };
    return style;
  }
  public toggleExpandGroup(group) {
    console.log('onToggleExpandGroup', group);
    this.markTable.groupHeader.toggleExpandGroup(group);
  }


  public onDetailToggle(event) {
    console.log('onDetailToggle', event);
  }
}

