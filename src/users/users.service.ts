import { Injectable } from '@nestjs/common';
import { IUsers } from './interfaces/users.interface';

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

  async create(user: IUsers): Promise<any> {
    this.users.push(user);
    console.log(this.users);
    const result = { ...user };
    delete result.password;
    return result;
  }
}
