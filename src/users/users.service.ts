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
    return this.usersModel.findOne({ username: username }).exec();
  }

  async create(user: IUsers): Promise<Users> {
    const createdUser = new this.usersModel(user);
    return createdUser.save();
  }

  async addCourseId(username: string, addedCourseId: number): Promise<any> {
    return this.usersModel
      .updateOne({ username }, { $push: { selectedCourses: addedCourseId } })
      .exec();
  }

  async removeCourseId(username: string, removeCourseId: number): Promise<any> {
    return this.usersModel
      .updateOne({ username }, { $pull: { selectedCourses: removeCourseId } })
      .exec();
  }

  async getUserProfile(username: string): Promise<IUserProfile> {
    const user = await this.findOne(username);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    const userObj = user.toObject();
    delete userObj.password;
    delete userObj._id;
    delete userObj.__v;
    userObj.statusCode = 200;
    return userObj;
  }

  async updateUserProfile(username: string, body: Users): Promise<object> {
    const user = await this.findOne(username);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    if (body.password) {
      user.password = body.password;
    }
    if (body.nickname) {
      user.nickname = body.nickname;
    }
    if (
      body.qualification &&
      ['undergraduate', 'bachelor', 'doctor'].includes(body.qualification)
    ) {
      user.qualification = body.qualification;
    }
    if (body.major && typeof body.major === 'string') {
      user.major = body.major;
    }
    await user.save();

    return {
      statusCode: 201,
      message: 'Profile updated successfully',
    };
  }
}
