import { Injectable, UnauthorizedException } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';
import { Users, UserDocument } from './schemas/users.schema';
import { IUsers } from './interfaces/users.interface';
import { IUserProfile } from './dto/users.dto';

@Injectable()
export class UsersService {
  constructor(
    @InjectModel(Users.name) private readonly usersModel: Model<UserDocument>,
  ) {}

  async findOne(username: string): Promise<any | undefined> {
    return this.usersModel.findOne({ username }).exec();
  }

  async create(user: IUsers): Promise<Users> {
    const createdUser = new this.usersModel(user);
    return createdUser.save();
  }

  async getUserProfile(username: string): Promise<IUserProfile> {
    const user = await this.findOne(username);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    const userObj = user.toObject();
    delete userObj.password;
    return userObj;
  }
}
