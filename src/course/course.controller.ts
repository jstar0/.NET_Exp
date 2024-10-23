import {
  BadRequestException,
  Body,
  Controller,
  Get,
  Param,
  Post,
  Req,
  UseGuards,
} from '@nestjs/common';
import { CourseService } from './course.service';
import { JwtAuthGuard } from '../auth/jwt-auth.guard';
import { ICourse } from './interfaces/course.interface';
import { IUsers } from 'src/users/interfaces/users.interface';

@Controller('courses')
export class CourseController {
  constructor(private readonly courseService: CourseService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  async getDefaultMessage() {
    return this.courseService.getDefaultMessage();
  }

  @UseGuards(JwtAuthGuard)
  @Get('list')
  async getCourseList() {
    return await this.courseService.getCourseList();
  }

  @UseGuards(JwtAuthGuard)
  @Get('selected')
  async getSelectedCourses(@Req() req: any) {
    return await this.courseService.getSelectedCourses(req.username);
  }

  /*   @UseGuards(JwtAuthGuard)
  @Get('search/courseId/:id')
  async matchCourseId(@Param('id') courseId: string) {
    return await this.courseService.matchCourseId(courseId);
  } */

  @UseGuards(JwtAuthGuard)
  @Get('search/major/:major')
  async searchCourseByMajor(@Param('major') major: string) {
    return await this.courseService.searchCourseByMajor(major);
  }

  /*   @UseGuards(JwtAuthGuard)
  //Get by q and major
  @Get('search/qamajor/:q/:major')
  async searchCourseByQualificationAndMajor(
    @Param('q') qualification: string,
    @Param('major') major: string,
  ) {
    return await this.courseService.searchCourseByQualificationAndMajor(
      qualification,
      major,
    );
  } */

  @UseGuards(JwtAuthGuard)
  @Post('admin/create')
  async createCourse(@Body() course: ICourse) {
    if (await this.courseService.createCourse(course))
      return 'Course created successfully';
    else throw new BadRequestException('Course creation failed');
  }

  @UseGuards(JwtAuthGuard)
  @Post('admin/delete')
  async deleteCourse(@Body('id') id: number) {
    if (await this.courseService.deleteCourse(id))
      return 'Course deleted successfully';
    else throw new BadRequestException('Course deletion failed');
  }

  @UseGuards(JwtAuthGuard)
  @Post('update')
  async updateCourse(@Body() body: IUsers, @Req() req: any) {
    return await this.courseService.updateCourseSelection(req.username, body);
  }
}
