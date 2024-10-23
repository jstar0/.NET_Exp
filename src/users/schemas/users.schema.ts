import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';
import { IUsers } from '../interfaces/users.interface';

export type UserDocument = Users & Document;

class BasicInfo {
  @Prop()
  name: string;

  @Prop()
  age: number;

  @Prop()
  gender: string;

  @Prop()
  ancestry: string;

  @Prop()
  political: string;

  @Prop()
  qualification: string;

  @Prop()
  major: string;

  @Prop()
  contact: string;

  @Prop()
  profile: string;
}

class FamilyInfo {
  @Prop()
  birth: string;

  @Prop()
  living: string;

  @Prop()
  zipCode: string;

  @Prop()
  homePhone: string;

  @Prop()
  address: string;

  @Prop()
  livingInCity: boolean;
}

@Schema()
export class Users extends Document implements IUsers {
  @Prop()
  username: string;

  @Prop()
  password: string;

  @Prop()
  schoolId: string;

  @Prop({ type: BasicInfo })
  basic: BasicInfo;

  @Prop()
  selectedCourses: number[];

  @Prop({ type: FamilyInfo })
  family: FamilyInfo;

  @Prop()
  photo: string;
}

export const UserSchema = SchemaFactory.createForClass(Users);
