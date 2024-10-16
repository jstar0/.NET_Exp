export class IUserProfile {
  username: string;
  nickname?: string;
  qualification?: 'undergraduate' | 'bachelor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
