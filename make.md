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

### 拉伸 Grid

- 将 Grid 作为页面的根元素：确保 Grid 不是嵌套在 StackPanel 等控件中，否则会影响其拉伸行为。
- 移除不必要的 HorizontalAlignment 和 VerticalAlignment：Grid 作为根元素时，会默认填充整个页面，子元素也会根据 Grid 行的定义自动拉伸，因此可以省略一些对齐属性。




# 具体操作

## 批量更改 Button 的 FontSize

使用共享的 FontSize 属性绑定。

1. 在 CalculatorStandardPage.xaml.cs 中 添加一个 NumpadButtonFontSize 属性

```csharp
public sealed partial class CalculateStandardPage : Page
{
    public double ButtonFontSize
    {
        get => (double)GetValue(ButtonFontSizeProperty);
        set => SetValue(ButtonFontSizeProperty, value);
    }

    public static readonly DependencyProperty ButtonFontSizeProperty =
        DependencyProperty.Register(
            nameof(ButtonFontSize),
            typeof(double),
            typeof(CalculateStandardPage),
            new PropertyMetadata(18.0));
    // ...
}
```

2. 在 CalculatorStandardPage.xaml 中 设定 x:Name="Root"

```xml
<Page
    ...
    x:Name="Root">
```

3. 绑定 NumpadButtonFontSize 到 Button 的 FontSize

```xml
<Button
    FontSize="{Binding NumpadButtonFontSize, ElementName=Root}" />
```

4. 在 VisualStateManager 中更改 NumpadButtonFontSize

```xml
<VisualState x:Name="FontResize0">
    <VisualState.StateTriggers>
        <AdaptiveTrigger MinWindowHeight="0" />
    </VisualState.StateTriggers>
    <VisualState.Setters>
        <Setter Target="Root.NumpadButtonFontSize" Value="14" />
    </VisualState.Setters>
</VisualState>
```

## 历史记录服务和模型

### 创建 HistoryModel 在 Calculator.Core/Models 中

HistoryModel.cs

```csharp
public class HistoryModel
{
    public string Expression { get; set; }
    public string Result { get; set; }
}
```

### IHistoryService 接口

```csharp
// File: Calculator.Core/Services/IHistoryService.cs
using System.Collections.ObjectModel;
using Calculator.Core.Models;

namespace Calculator.Core.Services
{
    public interface IHistoryService
    {
        ObservableCollection<HistoryModel> History { get; }

        void AddHistory(string expression, string result);
    }
}
```

### Create a HistoryService to manage history data.

```csharp
   // File: Calculator.Core/Services/HistoryService.cs
   using System.Collections.ObjectModel;
   using Calculator.Core.Models;

   namespace Calculator.Core.Services;

   public class HistoryService
   {
       private readonly ObservableCollection<HistoryModel> _history = new();

       public ObservableCollection<HistoryModel> History => _history;

       public void AddHistory(string expression, string result)
       {
           _history.Add(new HistoryModel { Expression = expression, Result = result });
       }

       // Methods for loading and saving history can be added here for persistence
   }
```

### Register the HistoryService in the App.xaml.cs

```csharp
   // File: Calculator/App.xaml.cs
   using Calculator.Core.Services;

   public App()
   {
       // Register the HistoryService as a singleton
        services.AddSingleton<IHistoryService, HistoryService>();

       // Other initialization code
   }
```

### 统一激活服务

在 Calculator/Services/ActivationService.cs 中

```csharp
private readonly IHistoryService _historyService;

public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService, ILanguageService languageService, IHistoryService historyService)
{
    _historyService = historyService;
}
```


### Inject HistoryService into your ViewModel.

```csharp
   // File: Calculator/ViewModels/CalculateStandardViewModel.cs
   using Calculator.Core.Services;
   using Calculator.Core.Models;
   using System.Collections.ObjectModel;

   namespace Calculator.ViewModels;

   public partial class CalculateStandardViewModel : ObservableRecipient
   {
       private readonly IHistoryService _historyService;

       public ObservableCollection<HistoryModel> History => _historyService.History; // History Should Be a Property, Not a Field

       public CalculateStandardViewModel(IHistoryService historyService) // constructor with dependency injection
       {
           _historyService = historyService;
       }

       // Method to perform calculation and add to history
       public void PerformCalculation(string expression)
       {
           // Perform calculation logic...
           string result = /* calculation result */;

           // Add to history
           _historyService.AddHistory(expression, result);
       }
   }
```

### HistoryPage.xaml.cs 中绑定使用上面这个 ViewModel

上面这个ViewModel是在CalculatorStandardPage.xaml.cs中创建的，所以需要在HistoryPage.xaml.cs中绑定这个ViewModel。

```csharp
   // File: Calculator/Views/HistoryPage.xaml.cs
    public sealed partial class HistoryPage : Page
    {
        public CalculateStandardViewModel ViewModel
        {
            get;
        }

        public HistoryPage()
        {
            ViewModel = App.GetService<CalculateStandardViewModel>();

            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
    // ...
```

###	Bind the history data to your UI.

```xml
   <!-- File: Calculator/Views/CalculateStandardPage.xaml -->
   <Page
        >

       <Grid>
           <!-- Other UI elements -->

           <ListView 
                ItemsSource="{x:Bind ViewModel.History, Mode=OneWay}" >
               <ListView.ItemTemplate>
                   <DataTemplate x:DataType="models:HistoryModel">
                    <!-- ... -->
                   </DataTemplate>
               </ListView.ItemTemplate>
           </ListView>
       </Grid>
   </Page>
```

### 事件处理

如果历史记录为空就显示 No history available，隐藏 Clear Button

同时设置清除事件

```csharp
    // File: Calculator/ViewModels/CalculateStandardViewModel.cs
    public sealed partial class HistoryPage : Page
    {


        public HistoryPage()
        {
            ViewModel.History.CollectionChanged += HistoryItems_CollectionChanged;
        }
    }

    private void HistoryItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateHistoryEmptyVisibility();
    }

    private void UpdateHistoryEmptyVisibility()
    {
        HistoryEmptyNotice.Visibility = ViewModel.History.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ClearHistoryButton.Visibility = ViewModel.History.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ClearHistory_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.History.Clear();
    }


```

### 修复 CalculatorStandardViewModel 未正确初始化使 XAML 加载崩溃的问题

问题的修复在于 HistoryService 在 ViewModel 未正确初始化。

应该修改 CalculatorStandardViewModel 的 constructor，不使用 GetService 而是使用依赖注入。


## 按键

### 绑定键盘按键

`IsTabStop="True"`

https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/keyboard-events

使用

```xml
<Button
    x:Name="ButtonMc"
    Grid.Column="0"
    Command="{Binding ButtonMcCommand}"
    FontSize="{Binding MemoryButtonFontSize, ElementName=Root}"
    Style="{StaticResource CalculatorMemoryButton}">
    <Button.KeyboardAccelerators>
        <KeyboardAccelerator
            Key="L"
            Invoked="OnButtonMemoryInvoked"
            Modifiers="Control" />
    </Button.KeyboardAccelerators>
    MC
</Button>
```
绑定。此时
```cs
private void OnButtonMemoryInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
{
    // Ensure the code runs on the UI thread
    DispatcherQueue.TryEnqueue(async () =>
    {
        if (args.Element is Button button)
        {
            // Change the visual state to 'Pressed'
            VisualStateManager.GoToState(button, "Pressed", true);

            // Execute the command associated with the button
            if (button.Command != null && button.Command.CanExecute(button.CommandParameter))
            {
                button.Command.Execute(button.CommandParameter);
            }

            // Wait briefly to display the 'Pressed' state
            await Task.Delay(100);

            // Change the visual state back to 'Normal'
            VisualStateManager.GoToState(button, "Normal", true);
        }
    });
    args.Handled = true;
}
```
`args.Element` 就是 `Button`。

### 触发

使用 UI Automation 模拟按键被点击的效果是个好主意，但是无法使动画持久显现（如等待100ms）

```cs
// Get the automation peer for the button
var peer = FrameworkElementAutomationPeer.FromElement(ButtonMc) ?? FrameworkElementAutomationPeer.CreatePeerForElement(ButtonMc);

if (peer is ButtonAutomationPeer buttonPeer)
{
    // Invoke the button programmatically
    buttonPeer.Invoke();
}
```

因此改为使用 `VisualStateManager`

```cs
// Change the visual state to 'Pressed'
VisualStateManager.GoToState(ButtonMc, "Pressed", true);

// Wait briefly to display the 'Pressed' state
await Task.Delay(100);

// Execute the command associated with the button
ViewModel.ButtonMcCommand.Execute(null);

// Change the visual state back to 'Normal'
VisualStateManager.GoToState(ButtonMc, "Normal", true);
```

由于 `Pressed` 等早已定义好，我们直接使用即可 =>

默认的按钮样式在

`%UserProfile%\.nuget\packages\microsoft.windowsappsdk\1.6.241114003\lib\uap10.0\Microsoft.UI\Themes`

### ToolTip

为 Button 修改 ToolTip

```xml
<Button
    ToolTipService.ToolTip="(记忆调用) Ctrl+R">
```