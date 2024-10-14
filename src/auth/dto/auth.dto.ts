export interface LoginUserDto {
  username: string;
  password: string;
}

export interface RegisterUserDto {
  username: string;
  password: string;
  nickname?: string;
  qulification?: 'undergraduate' | 'bachlor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
