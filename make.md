# 对于一个Behavior, Contracts, Helper, Model, Service, String, ViewModel, View的WinUI3设计模式，要创建一个计算器，应该添加哪些东西？

Behavior: 用于处理用户交互，例如按钮点击、输入验证等。
Contracts: 定义界面与实现之间的契约，确保数据流和功能隔离。
Helper: 提供辅助功能，例如数学计算、历史记录管理等。
Model: 处理计算器的核心逻辑，如加减乘除等数学操作。
Service: 可能用于获取设置、历史记录或其他外部资源。
String: 处理与显示相关的字符串格式化或转换，如数字显示或计算结果。
ViewModel: 绑定数据到界面，例如显示当前输入、历史记录等。
View: 定义UI界面，呈现按钮、输入框、结果等元素。

Calculator.Core 项目是一个独立的核心库，它应该包含与应用程序的核心业务逻辑和计算相关的代码。这部分代码应该是与UI无关的，因此它可以在多个应用程序项目中重复使用，符合跨平台和跨应用共享代码的设计目标。

应该在Calculator.Core中实现Model、Service、Helper等功能，例如Calculator类用于处理数学计算，HistoryService类用于管理历史记录，MathHelper类用于提供数学计算辅助方法。

# Calculator.Core 中（核心库）

## 创建 Model 和一些 Helper

## 注册历史记录服务

# 在 Calculator 中（UI）

## ViewModel 绑定数据到界面

## Behavior 处理用户交互

## View 定义 UI 界面

- 将 Grid 作为页面的根元素：确保 Grid 不是嵌套在 StackPanel 等控件中，否则会影响其拉伸行为。
- 移除不必要的 HorizontalAlignment 和 VerticalAlignment：Grid 作为根元素时，会默认填充整个页面，子元素也会根据 Grid 行的定义自动拉伸，因此可以省略一些对齐属性。
