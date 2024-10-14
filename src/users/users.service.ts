import { Injectable, UnauthorizedException } from '@nestjs/common';
import { IUsers } from './interfaces/users.interface';
import { IUserProfile } from './dto/users.dto';

@Injectable()
export class UsersService {
  private readonly users: IUsers[] = [
    {
      username: 'test',
      password: '$2a$10$J8OUwvXoyGExqEKC4yFDDuww8TifYO71GC5zFCLBBEpDpVqUlY9wK',
      nickname: 'test',
      qulification: 'undergraduate',
      major: 'test',
      selectedCourses: [1, 2],
    },
  ];

  async findOne(username: string): Promise<any | undefined> {
    return this.users.find((user) => user.username === username);
  }

  async create(user: IUsers): Promise<boolean> {
    this.users.push(user);
    //console.log(this.users);
    const result = { ...user };
    delete result.password;
    return true;
  }

  async getUserProfile(username: string): Promise<IUserProfile> {
    const user = await this.findOne(username);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    delete user.password;
    return user;
  }
}
