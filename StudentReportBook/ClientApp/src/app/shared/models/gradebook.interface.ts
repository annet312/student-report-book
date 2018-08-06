
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
