import { Injectable } from '@nestjs/common';

@Injectable()
export class UsersService {
  private readonly users = [
    {
      userId: 1,
      username: 'john',
      password: 'changeme', // 在实际应用中要加密
    },
    {
      userId: 2,
      username: 'maria',
      password: 'guess',
    },
  ];

  async findOne(username: string): Promise<any | undefined> {
    return this.users.find((user) => user.username === username);
  }
}
