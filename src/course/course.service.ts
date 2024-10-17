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
import { UsersService } from 'src/users/users.service';

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

  async selectCourse(
    selectedCourseId: number,
    username: string,
  ): Promise<ICourse> {
    if (!selectedCourseId || isNaN(selectedCourseId) || selectedCourseId < 0) {
      throw new NotAcceptableException('Invalid course ID');
    }
    // 从数据库中寻找 是否有 courseId 为 selectedCourseId 的课程
    const selectedCourse = this.matchCourseId(selectedCourseId.toString());
    if (!selectedCourse) {
      throw new NotAcceptableException('Course not found');
    }
    // 用户信息有效否
    const userProfile = await this.usersService.getUserProfile(username);
    if (!userProfile) {
      throw new UnauthorizedException('Invalid credentials');
    }
    // 查找用户是否已经选择了这门课
    const userCourseList = userProfile.selectedCourses;
    if (userCourseList.includes(selectedCourseId)) {
      throw new NotAcceptableException('Course already selected');
    }

    // 添加 courseId 到用户的 profile
    if (await this.usersService.addCourseId(username, selectedCourseId))
      return selectedCourse;
    else throw new NotAcceptableException('Course selection failed');
  }

  async deselectCourse(
    deselectedCourseId: number,
    username: string,
  ): Promise<ICourse> {
    if (
      !deselectedCourseId ||
      isNaN(deselectedCourseId) ||
      deselectedCourseId < 0
    ) {
      throw new NotAcceptableException('Invalid course ID');
    }
    const deselectedCourse = this.matchCourseId(deselectedCourseId.toString());
    if (!deselectedCourse) {
      throw new NotAcceptableException('Course not found');
    }

    const userProfile = await this.usersService.getUserProfile(username);
    if (!userProfile) {
      throw new UnauthorizedException('Invalid credentials');
    }
    // 查找用户是否已经选择了这门课

    const userCourseList = userProfile.selectedCourses;
    if (!userCourseList.includes(deselectedCourseId)) {
      throw new NotAcceptableException('Course not selected yet');
    }

    // 从用户的 profile 中删除 courseId
    if (await this.usersService.removeCourseId(username, deselectedCourseId))
      return deselectedCourse;
    else throw new NotAcceptableException('Course deselection failed');
  }

  async getDefaultMessage(): Promise<string> {
    return 'Here comes the Select Module!';
  }

  async getCourseList(): Promise<ICourse[]> {
    return this.courseModel.find().exec();
  }
}
