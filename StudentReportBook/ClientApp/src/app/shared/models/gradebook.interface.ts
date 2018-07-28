
export interface GradeBook {
  student: Student;
  marks: Mark[];
}

export interface Mark {
  subject: string;
  teacher: string;
  term: number;
  grade: number;
}

export interface Student {
  name: string;
  faculty: string;
  studentCard: string;
  currentTerm: number;
  group: string;
}
