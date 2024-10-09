const User = require('../models/User');

// 用户注册逻辑
const registerUser = async (req, res) => {
  try {
    const { name, email, password } = req.body;
    console.log("[registerUser] Got an request:", name, "with email:", email, "and password:", password);
    // 检查用户是否已经存在
    let user = await User.findOne({ email });
    if (user) {
      return res.status(400).json({ message: 'User already exists' });
    }

    // 创建新用户
    user = new User({
      name,
      email,
      password, // 可以在此处添加密码加密（如使用 bcrypt）
    });

    await user.save();
    res.status(201).json({ message: 'User registered successfully' });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

// 用户登录逻辑
const loginUser = async (req, res) => {
  try {
    const { name, password } = req.body;
    console.log("[loginUser] Got an request:", name, "with password:", password);
    const user = await User.findOne({ name });
    if (!user) {
      return res.status(400).json({ message: 'Invalid username or password' });
    }

    // 检查密码是否匹配（在此可以添加 bcrypt.compare 的逻辑）
    if (password !== user.password) {
      return res.status(400).json({ message: 'Invalid username or password' });
    }

    res.status(200).json({ message: 'User logged in successfully', user });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

// 获取用户信息
const getUserProfile = async (req, res) => {
  try {
    // 简单示例中，不需要验证 token
    console.log("[getUserProfile] Got an request:", req.body.username);
    const userName = req.body.username;
    if (!userName) {
      return res.status(401).json({ message: 'Not authorized' });
    }
    const user = await User.findOne({ name: userName });
    if (!user) {
      return res.status(404).json({ message: 'User not found' });
    }

    res.json({ name: user.name, email: user.email, createdAt: user.createdAt });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

module.exports = { registerUser, loginUser, getUserProfile };
