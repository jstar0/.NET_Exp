export class LoginUserDto {
  username: string;
  password: string;
}

export class RegisterUserDto {
  username: string;
  password: string;
  nickname?: string;
  qualification?: 'undergraduate' | 'bachelor' | 'doctor';
  major?: string;
  selectedCourses?: number[];
}
