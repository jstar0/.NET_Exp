export class LoginUserDto {
  username: string;
  password: string;
}

export class RegisterUserDto {
  username: string;
  password: string;
  nickname?: string;
  qulification?: 'undergraduate' | 'bachlor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
