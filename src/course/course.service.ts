import {
  Inject,
  Injectable,
  NotAcceptableException,
  UnauthorizedException,
} from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model } from 'mongoose';
import { Course, CourseDocument } from './schemas/course.schema';
import { ICourse } from './interfaces/course.interface';
import { UsersService } from '../users/users.service';
import { IUsers } from '../users/interfaces/users.interface';

@Injectable()
export class CourseService {
  constructor(
    @InjectModel(Course.name)
    private readonly courseModel: Model<CourseDocument>,
    @Inject(UsersService) private readonly usersService: UsersService,
  ) {}

  async findOne(courseId: string): Promise<ICourse> {
    const findedCourse = await this.courseModel
      .findOne({ id: courseId })
      .select('-__v')
      .exec();
    if (!findedCourse) {
      return null;
    }
    const findedCourseObj = findedCourse.toObject();
    delete findedCourseObj._id;
    return findedCourseObj as ICourse;
  }

  async fineByMajor(major: string): Promise<ICourse[]> {
    const findedCourses = await this.courseModel
      .find({ major })
      .select('-__v')
      .exec();
    return findedCourses.map((course) => {
      const courseObj = course.toObject();
      delete courseObj._id;
      return courseObj as ICourse;
    });
  }

  async fineByQualification(qualification: string): Promise<ICourse[]> {
    const findedCourses = await this.courseModel
      .find({ qualification })
      .select('-__v')
      .exec();
    return findedCourses.map((course) => {
      const courseObj = course.toObject();
      delete courseObj._id;
      return courseObj as ICourse;
    });
  }

  async fineByQualificationAndMajor(
    qualification: string,
    major: string,
  ): Promise<ICourse[]> {
    const findedCourses = await this.courseModel
      .find({ qualification, major })
      .select('-__v')
      .exec();
    return findedCourses.map((course) => {
      const courseObj = course.toObject();
      delete courseObj._id;
      return courseObj as ICourse;
    });
  }

  async updateCourseSelection(username: string, body: IUsers): Promise<object> {
    const user = await this.usersService.findOne(username);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    if (body.selectedCourses && Array.isArray(body.selectedCourses)) {
      // 对每个 courseId 进行 checkAvailability 检查
      for (const courseId of body.selectedCourses) {
        if (!(await this.checkCourseAvailability(courseId, user))) {
          throw new NotAcceptableException('Not available course');
        }
      }
      // 去重并更新 user.selectedCourses
      user.selectedCourses = Array.from(new Set(body.selectedCourses));
    }
    await user.save();
    return {
      statusCode: 201,
      message: 'User profile updated successfully',
    };
  }

  async checkCourseAvailability(
    courseId: number,
    user: IUsers,
  ): Promise<boolean> {
    const course = await this.matchCourseId(courseId.toString());
    if (!course) {
      throw new NotAcceptableException('Course not found');
    }
    // 根据用户的 qualification 和 major 来检查是否有权限选这门课
    // 从数据库中获取用户的 profile
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    // 检查是否有权限选这门课
    if (
      user.qualification !== course.qualification ||
      user.major !== course.major
    ) {
      throw new NotAcceptableException('Not available course');
    }
    return true;
  }

  async getSelectedCourses(username: string): Promise<ICourse[]> {
    const userProfile = await this.usersService.getUserProfile(username);
    if (!userProfile) {
      throw new UnauthorizedException('Invalid credentials');
    }
    const selectedCourses = await Promise.all(
      userProfile.selectedCourses.map((courseId) =>
        this.matchCourseId(courseId.toString()),
      ),
    );
    return selectedCourses;
  }

  async matchCourseId(courseId: string): Promise<ICourse> {
    return await this.findOne(courseId);
  }

  async searchCourseByMajor(major: string): Promise<ICourse[]> {
    return await this.fineByMajor(major);
  }

  async searchCourseByQualification(qualification: string): Promise<ICourse[]> {
    return await this.fineByQualification(qualification);
  }

  async searchCourseByQualificationAndMajor(
    qualification: string,
    major: string,
  ): Promise<ICourse[]> {
    return this.fineByQualificationAndMajor(qualification, major);
  }

  async createCourse(course: ICourse): Promise<boolean> {
    if (!course.id || isNaN(course.id) || course.id < 0) {
      throw new NotAcceptableException('Invalid course ID');
    }
    // 检查是否已有同id的课程
    const existingCourse = await this.matchCourseId(course.id.toString());
    if (existingCourse) {
      throw new NotAcceptableException('Course ID already exists');
    }
    const createdCourse = new this.courseModel(course);
    createdCourse.save();
    return true;
  }

  async deleteCourse(courseId: number): Promise<boolean> {
    if (!courseId || isNaN(courseId) || courseId < 0) {
      throw new NotAcceptableException('Invalid course ID');
    }
    const existingCourse = await this.matchCourseId(courseId.toString());
    if (!existingCourse) {
      throw new NotAcceptableException('Course not found');
    }
    this.courseModel.deleteOne({ id: courseId }).exec();
    return true;
  }

  async getDefaultMessage(): Promise<string> {
    return 'Here comes the Select Module!';
  }

  async getCourseList(): Promise<ICourse[]> {
    return this.courseModel.find().exec();
  }
}
