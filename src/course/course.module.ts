import { Module } from '@nestjs/common';
import { CourseController } from './course.controller';
import { CourseService } from './course.service';
import { MongooseModule } from '@nestjs/mongoose';
import { Course, CourseSchema } from './schemas/course.schema';
import { UsersModule } from 'src/users/users.module';

@Module({
  exports: [CourseService],
  imports: [
    MongooseModule.forFeature([{ name: Course.name, schema: CourseSchema }]),
    UsersModule,
  ],
  controllers: [CourseController],
  providers: [CourseService],
})
export class CourseModule {}
