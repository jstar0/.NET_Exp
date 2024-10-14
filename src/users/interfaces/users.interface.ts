export interface IUsers {
  username: string;
  password: string;
  nickname?: string;
  qulification?: 'undergraduate' | 'bachlor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
