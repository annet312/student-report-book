
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
  studentcard: string;
  currentterm: number;
  group: string;
}
