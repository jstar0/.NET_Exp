const express = require('express');
const { registerUser, loginUser, getUserProfile } = require('../controllers/userController');
const router = express.Router();

// 用户注册
router.post('/register', registerUser);

// 用户登录
router.post('/login', loginUser);

// 获取用户信息（假设需要身份验证）
router.get('/profile', getUserProfile);

module.exports = router;
