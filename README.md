## 本 Repo 将记录个人 .NET 学习的一些源码

### 本分支是一个简单的 NestJS + MongoDB 后端

几个测试的cURL命令：

- 注册

        curl -X POST http://localhost:5000/auth/register -H "Content-Type: application/json" -d '{"username": "test", "password": "test", "nickname": "test"}'

- 登录

        curl -X POST http://localhost:5000/auth/login -H "Content-Type: application/json" -d '{"username": "test", "password": "test"}'

- 获取用户信息

        curl -X GET http://localhost:5000/users/profile -H "Authorization: Bearer $TOKEN"

### Auth

#### 在用户登录和获取用户信息时使用 JWT（JSON Web Token）。

其原理是将用户信息加密后生成一个 token，登录成功后会发送给客户端，存在 LocalStorage 中。每次请求时，都会带有这个 token。

前端明文地将密码发送给后端，后端再使用 `bcryptjs` 进行加密。这并不危险，也是最佳实践，参考 [CSDN一篇博文](https://blog.csdn.net/sinat_34820292/article/details/113750467)

- 使用 `@UseGuards()` 装饰器来实现路由守卫。

- 我在 `jwt-auth.guard.ts` 中的 `handleRequest` 方法给 `request` 添加 `username` 属性，这样在后续的请求中就可以直接使用 `request.username` 来获取用户名。

- [解析 JWT](https://jwt.io/)

#### 使用 `bcryptjs` 对用户密码进行加盐地加密。

### Rate Limit

不知为何，不可使用。后续修复。

- 使用 `nestjs-rate-limiter` 这个 Middleware 进行限流。

        npm install @nestjs/throttler

- `ThrottlerModule` 进行全局限制。

特别注意：市面上的文档都没有对 Throttler 的最新更改进行标注。我们应该转而使用

```
    ThrottlerModule.forRoot({
      throttlers: [
        {
          ttl: 60,
          limit: 20,
        },
      ],
    }),
```

- 装饰器 `@Throttle()` 进行局部限制。

### 有意思的细节

#### interface 和 DTO（Data Transfer Object）有不同。

interface 就是 interface，而 DTO 是 class。

interface 主要用于定义数据结构的类型。DTO 主要用于定义传输数据的结构，并进行数据验证。

interface 在编译时被移除，不会在运行时存在。DTO 在运行时存在，可以进行数据验证。

interface 不能进行数据验证。DTO 可以与 class-validator 等库一起使用，进行数据验证。

### To-do

- 尝试 ApiFox 等工具，设计 API 文档，然后使用 Swagger 生成 API 文档。生成框架、测试用例（Mock）后再写实现。

- 修复 Rate Limit。