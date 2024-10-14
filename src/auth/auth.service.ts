import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UsersService } from '../users/users.service';
import * as bcrypt from 'bcryptjs';

@Injectable()
export class AuthService {
  constructor(
    private authService: UsersService,
    private jwtService: JwtService,
  ) {}

  async validateUser(username: string, password: string): Promise<any> {
    const user = await this.authService.findOne(username);
    if (user && bcrypt.compareSync(password, user.password)) {
      const { password, ...result } = user;
      return result; // 返回不含密码的用户对象
    }
    return null;
  }

  async login(user: any) {
    const payload = { username: user.username, sub: user.userId };
    console.log('payload', payload);
    return {
      access_token: this.jwtService.sign(payload),
    };
  }
}
