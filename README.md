## 本 Repo 将记录个人 .NET 学习的一些源码

### 本分支是一个简单的 NestJS + MongoDB 后端

几个测试的cURL命令：

注册

    http://localhost:5000/auth/register

登录

    http://localhost:5000/auth/login

获取用户信息

    http://localhost:5000/users/profile


### Auth

- 在用户登录和获取用户信息时使用 JWT（JSON Web Token）或其他身份验证机制。

- 使用bcryptjs对用户密码进行加密。

### To-do

尝试 ApiFox 等工具，设计 API 文档，然后使用 Swagger 生成 API 文档。生成框架、测试用例（Mock）后再写实现。