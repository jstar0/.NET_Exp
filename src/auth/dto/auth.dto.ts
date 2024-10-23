export class LoginUserDto {
  username: string;
  password: string;
}

export class RegisterUserDto {
  schoolId: string;
  username: string;
  password: string;
}

export class ForgetUserDto {
  schoolId: string;
}
