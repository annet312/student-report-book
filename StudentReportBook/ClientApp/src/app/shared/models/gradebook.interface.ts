
export interface GradeBook {
  student: Student;
  marks: Mark[];
}

export interface Mark {
  id: number;
  subject: string;
  teacher: string;
  term: number;
  grade: number;
}

export interface Student {
  id: number;
  name: string;
  faculty: string;
  studentCard: string;
  currentTerm: number;
  group: string;
}

export interface Teacher {
  id: number;
  name: string;
  firstName: string;
  lastName: string;
  department: string;
}

export interface Faculty {
  id: number;
  name: string;
  groups: Group[];
}

export interface Group {
  id: number;
  name: string;
}

export interface TeacherWorkload {
  id: number;
  group: Group;
  term: number;
  subject: Subject;
}

export interface Subject {
  id: number;
  name: string;
}
