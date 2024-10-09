const User = require('../models/User');

// 用户注册逻辑
const registerUser = async (req, res) => {
  const { name, email, password } = req.body;

  try {
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
  const { email, password } = req.body;

  try {
    const user = await User.findOne({ email });
    if (!user) {
      return res.status(400).json({ message: 'Invalid email or password' });
    }

    // 检查密码是否匹配（在此可以添加 bcrypt.compare 的逻辑）
    if (password !== user.password) {
      return res.status(400).json({ message: 'Invalid email or password' });
    }

    res.status(200).json({ message: 'User logged in successfully', user });
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

// 获取用户信息
const getUserProfile = async (req, res) => {
  const userId = req.user.id;

  try {
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).json({ message: 'User not found' });
    }

    res.json(user);
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
};

module.exports = { registerUser, loginUser, getUserProfile };
