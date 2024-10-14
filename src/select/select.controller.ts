import { Controller, Get } from '@nestjs/common';
import { SelectService } from './select.service';

@Controller('select')
export class SelectController {
  constructor(private readonly selectService: SelectService) {}

  @Get()
  getDefaultMessage() {
    return this.selectService.getDefaultMessage();
  }
}
