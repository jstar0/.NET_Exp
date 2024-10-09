## 本 Repo 将记录个人 .NET 学习的一些源码

### 简单的 Node.js + Express + MongoDB 后端

几个测试的cURL命令：

注册

    curl -X POST http://localhost:5000/api/users/register -H "Content-Type: application/json" -d '{"name":"testuser","password":"testpassword", "email":"example@example.com"}'

登录

    curl -X POST http://localhost:5000/api/users/login -H "Content-Type: application/json" -d '{"name":"testuser","password":"testpassword"}'

获取用户信息

    curl -X GET http://localhost:5000/api/users/profile -H "Content-Type: application/json" -d '{"username":"testuser"}'


### To-Do

- 在用户登录和获取用户信息时使用 JWT（JSON Web Token）或其他身份验证机制。

- 使用bcryptjs对用户密码进行加密。

```npm install jsonwebtoken bcryptjs```

