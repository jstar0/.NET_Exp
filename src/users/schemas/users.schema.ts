import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';
import { IUsers } from '../interfaces/users.interface';

export type UserDocument = Users & Document;

@Schema()
export class Users extends Document implements IUsers {
  @Prop()
  username: string;

  @Prop()
  password: string;

  @Prop()
  nickname: string;

  @Prop()
  qualification: 'undergraduate' | 'bachelor' | 'doctor';

  @Prop()
  major: string;

  @Prop()
  selectedCourses: number[];
}

export const UserSchema = SchemaFactory.createForClass(Users);
