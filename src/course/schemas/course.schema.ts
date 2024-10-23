import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';
import { ICourse } from '../interfaces/course.interface';

export type CourseDocument = Course & Document;

@Schema()
export class Course extends Document implements ICourse {
  @Prop()
  id: number;

  @Prop()
  name: string;

  @Prop()
  major?: string;

  @Prop()
  description?: string;
}

export const CourseSchema = SchemaFactory.createForClass(Course);
