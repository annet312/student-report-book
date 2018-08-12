import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModerateTeacherComponent } from './moderate-teacher.component';

describe('ModerateTeacherComponent', () => {
  let component: ModerateTeacherComponent;
  let fixture: ComponentFixture<ModerateTeacherComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModerateTeacherComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModerateTeacherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
