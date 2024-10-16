import { Controller, Get, Post, UseGuards } from '@nestjs/common';
import { SelectService } from './course.service';
import { ICourse } from './interfaces/course.interface';
import { JwtAuthGuard } from '../auth/jwt-auth.guard';

@Controller('courses')
export class SelectController {
  constructor(private readonly selectService: SelectService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  getDefaultMessage() {
    return this.selectService.getDefaultMessage();
  }

  @UseGuards(JwtAuthGuard)
  @Get('list')
  getCourseList() {
    return this.selectService.getCourseList();
  }

  @UseGuards(JwtAuthGuard)
  @Get('detail')
  getCourseDetails() {
    return 'Course details';
  }

  @UseGuards(JwtAuthGuard)
  @Post('select')
  selectCourse() {
    return 'Course selected';
  }

  @UseGuards(JwtAuthGuard)
  @Post('deselect')
  deselectCourse() {
    return 'Course deselected';
  }
}
