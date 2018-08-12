import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModerateStudentComponent } from './moderate-student.component';

describe('ModerateStudentComponent', () => {
  let component: ModerateStudentComponent;
  let fixture: ComponentFixture<ModerateStudentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModerateStudentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModerateStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
