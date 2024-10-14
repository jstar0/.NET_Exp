import { Module } from '@nestjs/common';
import { SelectController } from './course.controller';
import { SelectService } from './course.service';

@Module({
  controllers: [SelectController],
  providers: [SelectService],
})
export class SelectModule {}
