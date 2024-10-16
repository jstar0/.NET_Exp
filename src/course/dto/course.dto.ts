export class CourseDTO {
  id: number;
  name: string;
  qualification: 'undergraduate' | 'bachelor' | 'doctor';
  major?: string;
  description?: string;
}

export class CourseListDTO {
  courses: CourseDTO[];
}
