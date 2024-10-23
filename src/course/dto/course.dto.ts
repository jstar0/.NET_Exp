export class CourseDTO {
  id: number;
  name: string;
  major?: string;
  description?: string;
}

export class CourseListDTO {
  courses: CourseDTO[];
}
