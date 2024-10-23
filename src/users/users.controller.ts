import { Controller, Get, Post, Req, UseGuards } from '@nestjs/common';
import { JwtAuthGuard } from '../auth/jwt-auth.guard';
import { UsersService } from './users.service';

@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @UseGuards(JwtAuthGuard)
  @Get('profile')
  async getProfile(@Req() req: any) {
    return await this.usersService.getUserProfile(req.username);
  }

  @UseGuards(JwtAuthGuard)
  @Post('profile')
  async updateProfile(@Req() req: any) {
    return await this.usersService.updateUserProfile(req.username, req.body);
  }

  @UseGuards(JwtAuthGuard)
  @Get('photo')
  async getPhoto(@Req() req: any) {
    return await this.usersService.getUserPhoto(req.username);
  }
}
