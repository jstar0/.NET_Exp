import { Module } from '@nestjs/common';
import { SelectController } from './course.controller';
import { SelectService } from './course.service';
import { MongooseModule } from '@nestjs/mongoose';
import { Course, CourseSchema } from './schemas/course.schema';

@Module({
  imports: [
    MongooseModule.forFeature([{ name: Course.name, schema: CourseSchema }]),
  ],
  controllers: [SelectController],
  providers: [SelectService],
})
export class SelectModule {}
