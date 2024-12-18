# 使标题栏横向

+ Add row definitions and separate the AppTitleBar and NavigationViewControl.
+ Add left margin to the AppTitleBar.
+ Remove NavigationViewControl_DisplayModeChanged event handler.

[参见 StackOverflow](https://stackoverflow.com/questions/75971358/templatestudio-winui-3-horizontal-navbar)

# TabbedCommandBar

```shell
dotnet add package CommunityToolkit.WinUI.Controls.TabbedCommandBar
# CommunityToolkit.WinUI.Sizer 等
dotnet add package CommunityToolkit.WinUI.Controls.ColorPicker
```

版本要对

```xml
<TargetFramework>net9.0-windows10.0.22621.0</TargetFramework>
<WindowsSdkPackageVersion>10.0.22621.38</WindowsSdkPackageVersion>
```

# 当加载 MainPage 时，启用（可见）ShellPage的 controls:TabbedCommandBar.MenuItems 离开 MainPage 时，禁用（不可见）之


# 页面加载事件

```xml
<Page
    OnLoaded="Page_Loaded"
    OnUnloaded="Page_Unloaded">
```

或

[链接](https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.frameworkelement.loaded?view=winrt-26100)
