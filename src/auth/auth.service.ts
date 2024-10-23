import { Injectable, UnauthorizedException } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UsersService } from '../users/users.service';
import { LoginUserDto, RegisterUserDto } from './dto/auth.dto';
import * as bcrypt from 'bcryptjs';
import { IUsers } from 'src/users/interfaces/users.interface';

@Injectable()
export class AuthService {
  constructor(
    private userService: UsersService,
    private jwtService: JwtService,
  ) {}

  async validateUser(username: string, password: string): Promise<any> {
    const user = await this.userService.findOne(username);
    if (user && bcrypt.compareSync(password, user.password)) {
      delete user.password;
      return user;
    }
    return null;
  }

  async login(user: LoginUserDto) {
    const validatedUser = await this.validateUser(user.username, user.password);
    if (!validatedUser) {
      throw new UnauthorizedException('Invalid credentials');
    }
    const payload = {
      username: validatedUser.username,
    };
    return {
      statusCode: 200,
      access_token: this.jwtService.sign(payload),
    };
  }

  async register(user: RegisterUserDto) {
    const findUser = await this.userService.findOne(user.username);
    if (findUser) {
      throw new UnauthorizedException('User already exists');
    }
    const hashedPassword = bcrypt.hashSync(user.password, 10);
    const newUser: IUsers = {
      username: user.username,
      password: hashedPassword,
      schoolId: user.schoolId,
    };
    if (!this.userService.create(newUser)) {
      throw new UnauthorizedException('Failed to create user');
    }
    const payload = {
      username: newUser.username,
    };
    return {
      statusCode: 201,
      access_token: this.jwtService.sign(payload),
    };
  }
}
