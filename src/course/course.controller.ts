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
  @Get('search/qualification/:q')
  async searchCourseByQualification(@Param('q') qualification: string) {
    return await this.courseService.searchCourseByQualification(qualification);
  }

  @UseGuards(JwtAuthGuard)
  @Get('search/courseId/:id')
  async matchCourseId(@Param('id') courseId: string) {
    return await this.courseService.matchCourseId(courseId);
  }

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
  @Post('select')
  async selectCourse(@Body('id') selectedCourseId: number, @Req() req: any) {
    return await this.courseService.selectCourse(
      selectedCourseId,
      req.username,
    );
  }

  @UseGuards(JwtAuthGuard)
  @Post('deselect')
  async deselectCourse(@Body('id') selectedCourseId: number, @Req() req: any) {
    return await this.courseService.deselectCourse(
      selectedCourseId,
      req.username,
    );
  }
}
