import { Controller, Post, Request } from '@nestjs/common';
import { AuthService } from './auth.service';

@Controller('auth')
export class AuthController {
  constructor(private authService: AuthService) {}

  @Post('login')
  async login(@Request() req: any) {
    return await this.authService.login(req.body);
  }

  @Post('register')
  async register(@Request() req: any) {
    return await this.authService.register(req.body);
  }
}
