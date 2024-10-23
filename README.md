## 本 Repo 将记录个人 .NET 学习的一些源码

### 本分支是一个简单的 NestJS + MongoDB 后端

几个测试的cURL命令：

- 注册

        curl -X POST http://localhost:5000/auth/register -H "Content-Type: application/json" -d '{"username": "test", "password": "test", "schoolId": "10000"}'

- 登录

        curl -X POST http://localhost:5000/auth/login -H "Content-Type: application/json" -d '{"username": "test", "password": "test"}'

- 获取用户信息

        curl -X GET http://localhost:5000/users/profile -H "Authorization: Bearer $TOKEN"

- 修改用户信息

        curl -X POST http://localhost:5000/users/profile -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"nickname": "test2"}'

- 获取完整的课程列表

        curl -X GET http://localhost:5000/courses/list -H "Authorization: Bearer $TOKEN"

- 按类型获取课程列表

        curl -X GET http://localhost:5000/courses/search/qualification/bachelor -H "Authorization: Bearer $TOKEN"

- 按ID获取课程

        curl -X GET http://localhost:5000/courses/search/courseId/1 -H "Authorization: Bearer $TOKEN"

- 按专业获取课程列表

        curl -X GET http://localhost:5000/courses/search/major/cs -H "Authorization: Bearer $TOKEN"

- 按 类型 和 专业 获取课程

        curl -X GET http://localhost:5000/courses/search/qamajor/bachelor/math -H "Authorization: Bearer $TOKEN"

- 获取已选课程信息

        curl -X GET http://localhost:5000/courses/selected -H "Authorization: Bearer $TOKEN"

- 选课和退课（更新选课信息）

        curl -X POST http://localhost:5000/courses/update -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"selectedCourses":[1,4,6,7]}'

- 创建课程

        curl -X POST http://localhost:5000/courses/admin/create -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"id": 1, "name": "信息系统开发(.NET)", "qualification": "undergraduate", "major": "cs", "description": "这是一门学习.NET的课程。"}'

- 移除课程

        curl -X POST http://localhost:5000/courses/admin/delete -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"id": 1}'


### Auth

#### 在用户登录和获取用户信息时使用 JWT（JSON Web Token）。

其原理是将用户信息加密后生成一个 token，登录成功后会发送给客户端，存在 LocalStorage 中。每次请求时，都会带有这个 token。

前端明文地将密码发送给后端，后端再使用 `bcryptjs` 进行加密。这并不危险，也是最佳实践，参考 [CSDN一篇博文](https://blog.csdn.net/sinat_34820292/article/details/113750467)

- 使用 `@UseGuards()` 装饰器来实现路由守卫。

- 我在 `jwt-auth.guard.ts` 中的 `handleRequest` 方法给 `request` 添加 `username` 属性，这样在后续的请求中就可以直接使用 `request.username` 来获取用户名。

- [解析 JWT](https://jwt.io/)

#### 使用 `bcryptjs` 对用户密码进行加盐地加密。

### MongoDB

#### 连接到 MongoDB 的方法

- 先创建对应的 Schemas 和 Models。参见 `/src/users/schemas/users.schema.ts`。

- Model 在 `/src/users/users.module.ts` 中导入，在 `/src/users/users.service.ts` 中使用。

#### 免费数据库

- 用 @jstar0 GitHub 账号

- 申请在 [MongoDB Atlas](cloud.mongodb.com) 上的免费数据库

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

#### Post 请求的 Content-Type

当客户端不使用 `application/json` 作为 Content-Type 时，后端无法将客户端的 Post 请求解析为 JSON 对象。

### 使用 Vercel 持续集成

不知为何，Vercel 对 NestJS 的支持并不好。采用 [这个](https://www.technog.com.br/blog/tips-and-tricks/how-to-deploy-a-nestjs-app-for-free-on-vercel/) 方法。

- 本地执行 `pnpm run build && vercel --prod`

### To-do

- 尝试 ApiFox 等工具，设计 API 文档，然后使用 Swagger 生成 API 文档。生成框架、测试用例（Mock）后再写实现。

- 修复 Rate Limit。