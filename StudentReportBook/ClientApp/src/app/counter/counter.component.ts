import { Component, Inject, ViewEncapsulation, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GradeBook, Student, Mark } from '../shared/models/gradebook.interface';
import { NgStyle } from '@angular/common';




@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'  
})
export class CounterComponent {
  public gradebook: GradeBook;

  @ViewChild('markTable') markTable: any;

  row = [];
  rowmarks = [];
  groups = [];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
   http.get<GradeBook>(baseUrl + 'api/student/getMygradeBook').subscribe(result => {
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
  ngOnInit() {

  }

  getGroupRowHeight(group, rowHeight) {
    let style = {};

    style = {
      height: (group.length * 40) + 'px',
      width: '100%'
    };

    return style;
  }

  public ToggleExpandGroup(group) {
    console.log('onToggleExpandGroup', group);
    this.markTable.groupHeader.toggleExpandGroup(group);
  }

  public onDetailToggle(event) {
    console.log('onDetailToggle', event);
  }
}

