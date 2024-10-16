export interface IUsers {
  username: string;
  password: string;
  nickname?: string;
  qualification?: 'undergraduate' | 'bachelor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
