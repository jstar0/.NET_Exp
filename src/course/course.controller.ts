import { Controller, Get, UseGuards } from '@nestjs/common';
import { SelectService } from './course.service';
import { JwtAuthGuard } from '../auth/jwt-auth.guard';

@Controller('select')
export class SelectController {
  constructor(private readonly selectService: SelectService) {}

  @UseGuards(JwtAuthGuard)
  @Get()
  getDefaultMessage() {
    return this.selectService.getDefaultMessage();
  }
}
