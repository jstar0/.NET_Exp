export class IUserProfile {
  username: string;
  nickname?: string;
  qulification?: 'undergraduate' | 'bachlor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
