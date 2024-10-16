import { Controller, Post, Body, UnauthorizedException } from '@nestjs/common';
import { AuthService } from './auth.service';
import { LoginUserDto, RegisterUserDto } from './dto/auth.dto';

@Controller('auth')
export class AuthController {
  constructor(private authService: AuthService) {}

  @Post('login')
  async login(@Body() loginUser: LoginUserDto) {
    if (!loginUser.password || !loginUser.username) {
      throw new UnauthorizedException('Username and password are required');
    }
    return await this.authService.login(loginUser);
  }

  @Post('register')
  async register(@Body() registerUser: RegisterUserDto) {
    if (!registerUser.password || !registerUser.username) {
      throw new UnauthorizedException('Username and password are required');
    }
    return await this.authService.register(registerUser);
  }
}
