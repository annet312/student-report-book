import { Pipe, PipeTransform } from '@angular/core';

import { GradeBook, Student, Mark } from '../models/gradebook.interface';

@Pipe({ name: 'flyingHeroes' })
export class MarkForTermPipe implements PipeTransform {
  transform(allMarks: Mark[], term) {
    return allMarks.filter(mark => mark.term == term);
  }
}
