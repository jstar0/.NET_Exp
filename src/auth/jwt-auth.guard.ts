import {
  Injectable,
  ExecutionContext,
  UnauthorizedException,
} from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';

@Injectable()
export class JwtAuthGuard extends AuthGuard('jwt') {
  canActivate(context: ExecutionContext) {
    return super.canActivate(context);
  }

  handleRequest(
    err: any,
    user: any,
    info: { message: any },
    context: ExecutionContext,
  ) {
    if (err || !user) {
      throw err || new UnauthorizedException(info?.message || 'Unauthorized');
    }

    const request = context.switchToHttp().getRequest();
    request.username = user.username;

    return user;
  }
}
