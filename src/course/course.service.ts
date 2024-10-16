import { Injectable } from '@nestjs/common';

@Injectable()
export class SelectService {
  getDefaultMessage(): string {
    return 'Here comes the Select Module!';
  }

  getCourseList(): string {
    return 'List of courses';
  }
}
