
# 切换语言：

https://cloud.tencent.com/developer/ask/sof/106544177
https://stackoverflow.com/questions/70440433/winui-3-runtime-localization

```cs
private void ReloadLanguage(string languageOverride)
{
    // Change the app language
    Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = languageOverride;

    // Be sure to clear the Frame stack so that cached Pages are removed, otherwise they will have the old language.
    Frame.BackStack.Clear();

    // Reload the page that you want to have the new language
    Frame.Navigate(typeof(MainPage));

}
```


# 存储的配置文件位置：

C:\Users\jstar0\AppData\Local\Packages\524ba3d3-f643-4af8-8e73-bf8268139ef9_dfnyvm30mvsfr\Settings\settings.dat

其中524ba3d3-f643-4af8-8e73-bf8268139ef9_dfnyvm30mvsfr是应用的包名，可以在Package.appxmanifest中找到。


# 创建一个 LanguageService：

## 首先定义一个接口：

在 Contracts 文件夹下 Services 文件夹下创建 ILanguageService.cs 文件：

## 然后实现这个接口：

在 Services 文件夹下创建 LanguageService.cs 文件

在相关的实现中，其实是在构造时，利用 ILocalSettingsService 读取设置，然后在设置时，利用 ILocalSettingsService 保存设置。

## 写一个 Helper 用于更改语言

在 Helpers 文件夹下创建 LanguageHelper.cs 文件

## 最后，在 App.xaml.cs 中注册这个服务：

```cs
            // Services
            services.AddSingleton<ILanguageService, LanguageService>();
```

## 并且在 ActivationService.cs 中使用这个服务：

```cs
    private readonly ILanguageService _languageService;

    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService, ILanguageService languageService)
    {
        _languageService = languageService;
    }

    private async Task InitializeAsync()
    {
        await _languageService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        await _languageService.SetRequestedLanguageAsync();
        await Task.CompletedTask;
    }
```





# MVVM 模式：

## 概述

MVVM（Model-View-ViewModel）是一种常见的设计模式，特别适用于现代桌面和移动应用程序开发，尤其是在 WPF、Xamarin、WinUI 等框架中。它的目标是通过分离关注点，促进代码的可维护性、可测试性和可扩展性。MVVM 模式将应用程序的不同功能划分成三部分：**Model**, **View**, 和 **ViewModel**。下面是对 MVVM 模式各个部分的详细说明：

### 1. **Model（模型）**
   - **职责**：`Model` 代表应用程序中的核心数据和业务逻辑。它是与应用程序的业务功能和数据存储有关的部分，可以包括数据类、数据库操作、业务规则等。
   - **特点**：
     - `Model` 是纯粹的业务数据，通常与界面（`View`）无关。
     - 不包含任何显示或 UI 逻辑，只关注数据的处理和存储。
     - 可以包括对外部服务（如 Web API）的调用、数据库操作、文件处理等。
   - **示例**：一个简单的 `Product` 类，可能有属性如 `ProductName`, `Price`, `StockCount`，以及用于验证库存或计算价格折扣的方法。

   ```csharp
   public class Product
   {
       public string ProductName { get; set; }
       public decimal Price { get; set; }
       public int StockCount { get; set; }
       
       // 业务逻辑
       public bool IsInStock() => StockCount > 0;
   }
   ```

### 2. **View（视图）**
   - **职责**：`View` 是应用程序的用户界面部分，负责呈现 `ViewModel` 提供的数据并接收用户输入。`View` 是 UI 的表现层，通常包含布局、控件（按钮、文本框、列表等）和动画等。
   - **特点**：
     - `View` 只关心显示内容，不包含业务逻辑。
     - 与 `ViewModel` 通过数据绑定进行交互。
     - 在 MVVM 模式中，`View` 应该尽量避免直接引用或处理 `Model`，它依赖 `ViewModel` 提供的接口和属性。
     - 一般来说，`View` 会通过命令（Commands）来处理用户的交互，而不是直接处理逻辑。
   - **示例**：在 XAML 中定义一个按钮，它绑定到 `ViewModel` 中的 `LoginCommand` 命令：

     ```xml
     <Button Content="Login" Command="{Binding LoginCommand}" />
     ```

   `View` 和 `ViewModel` 之间的绑定使得 `View` 可以自动更新，反映 `ViewModel` 中数据的变化，而不需要 `View` 直接操作 `Model`。

### 3. **ViewModel（视图模型）**
   - **职责**：`ViewModel` 是 `View` 和 `Model` 之间的中介，负责将 `Model` 中的数据转化为 `View` 可以显示的格式，并处理用户输入的命令。`ViewModel` 还管理应用的视图状态（如正在加载、显示错误等）。
   - **特点**：
     - `ViewModel` 包含 `Model` 的数据，但不依赖于 UI 相关的类。
     - 通过数据绑定（Data Binding）与 `View` 交互，向 `View` 提供数据。
     - 它处理用户交互（如按钮点击），并触发相应的命令和事件。
     - 通常会实现 `INotifyPropertyChanged` 接口，以便当属性的值发生变化时通知 `View` 进行更新。
     - `ViewModel` 不直接操作 `Model`，而是通过服务或其他业务逻辑来获取或修改数据。
   - **示例**：一个 `LoginViewModel`，它可能包含用户名、密码字段和登录命令，并通过服务验证用户身份。

     ```csharp
     public class LoginViewModel : INotifyPropertyChanged
     {
         private string _username;
         private string _password;
         private ICommand _loginCommand;

         public string Username
         {
             get => _username;
             set { _username = value; OnPropertyChanged(); }
         }

         public string Password
         {
             get => _password;
             set { _password = value; OnPropertyChanged(); }
         }

         public ICommand LoginCommand
         {
             get
             {
                 if (_loginCommand == null)
                 {
                     _loginCommand = new RelayCommand(async () => await LoginAsync());
                 }
                 return _loginCommand;
             }
         }

         private async Task LoginAsync()
         {
             // 调用服务来验证用户登录
             bool isValid = await _authenticationService.LoginAsync(Username, Password);
             if (isValid)
             {
                 // 登录成功，做相应处理
             }
             else
             {
                 // 登录失败，显示错误
             }
         }

         public event PropertyChangedEventHandler PropertyChanged;

         protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }
     }
     ```

### MVVM 的优点

1. **分离关注点**：通过将 UI（`View`）与业务逻辑（`Model`）和数据操作（`ViewModel`）分离，MVVM 实现了良好的解耦，使得每一部分都可以独立开发和测试。
2. **提高可维护性**：`ViewModel` 作为中介层，允许你在不改变 UI 代码的情况下修改业务逻辑，同时 `Model` 的修改不会直接影响到 UI。
3. **提高可测试性**：`ViewModel` 可以单独进行单元测试，因为它不依赖于 UI 界面，测试可以直接集中在业务逻辑和数据上。
4. **数据绑定**：数据绑定机制减少了大量的 UI 更新代码，`View` 会自动根据 `ViewModel` 的状态更新界面。
5. **易于扩展**：由于各个部分独立，添加新功能时只需要关注相关的部分（如新增业务逻辑、修改界面等）。

### MVVM 的挑战

1. **学习曲线**：对于初学者来说，理解数据绑定和命令模式（`Command`）等概念可能需要一些时间。
2. **过度设计**：在一些简单的应用中，MVVM 可能显得过于复杂，特别是当项目规模较小时，可能会导致不必要的层次化和代码分离。
3. **调试和跟踪**：由于应用程序的逻辑分散在多个层次中，调试和跟踪问题可能需要在多个层次之间来回切换。

### 总结
MVVM 模式通过将应用程序分为三个主要部分：`Model`、`View` 和 `ViewModel`，促进了业务逻辑和 UI 的解耦。它提供了一种清晰的架构，特别适合复杂的应用程序。通过数据绑定和命令模式，MVVM 简化了 UI 的更新和用户交互的处理，同时提高了应用的可维护性、可扩展性和可测试性。

## ViewModel, Helper, Contracts, Model, Service

在 WinUI 3 程序中，设计良好的架构能够帮助你管理应用的不同部分，提高可维护性和可扩展性。你提到的 **ViewModel**, **Helper**, **Contracts**, **Model**, 和 **Service** 是常见的设计模式和概念，它们有助于分离不同的关注点。下面是对这些概念的具体解释：

### 1. **ViewModel**
   - **职责**：ViewModel 是 MVVM（Model-View-ViewModel）模式中的一部分，负责与 View（视图）进行交互，但不直接依赖于 View 的实现。它是 View 和 Model 之间的中介，将 Model 数据转化为 View 可以呈现的格式，并管理视图状态（如是否正在加载、是否显示错误信息等）。
   - **特点**：
     - 它包含视图所需的所有数据和状态。
     - 通过数据绑定（Data Binding）与 View 进行交互。
     - 可以实现逻辑处理，比如数据验证、命令执行等。
     - 通常会实现 `INotifyPropertyChanged` 接口，以便当属性变化时通知视图更新。
   - **示例**：比如一个 `LoginViewModel` 可能包含用户名、密码字段，和一个 `LoginCommand` 命令。

### 2. **Helper**
   - **职责**：Helper 类通常包含一些通用的辅助方法，帮助简化重复的任务或功能。它们不涉及业务逻辑，通常是对某些功能的封装，以便复用和减少代码重复。
   - **特点**：
     - 辅助方法通常是静态的。
     - 用于一些常见的操作，比如格式化日期、计算一些数值、处理文件路径等。
   - **示例**：一个 `StringHelper` 类可能有一个方法来验证邮箱格式，或一个 `DateHelper` 类来格式化日期。

### 3. **Contracts**
   - **职责**：Contracts 通常指代应用程序中的接口或抽象类，它们定义了不同组件（如服务、数据访问层等）之间的协议。通过 Contracts，可以让代码更具松耦合性，便于替换实现或进行单元测试。
   - **特点**：
     - 定义接口（interface）或抽象类，用于规范方法和属性。
     - 提供一种标准化的方式与其他部分进行交互。
     - 支持依赖注入（Dependency Injection）和松耦合设计。
   - **示例**：一个 `IAuthenticationService` 接口，定义了 `Login()` 和 `Logout()` 方法。然后可以根据需要实现不同的认证方式，如通过本地数据库、第三方 API 等。

### 4. **Model**
   - **职责**：Model 是表示应用程序数据和业务逻辑的部分。它通常对应于应用程序的核心数据结构，或者是与数据库交互的部分。Model 不关心视图如何呈现数据，它只是纯粹的数据结构和处理逻辑。
   - **特点**：
     - 可能包含业务逻辑、验证规则、数据持久化等。
     - 可以是简单的数据类，或者更复杂的服务类。
     - 在某些情况下，Model 可以直接与数据库或远程服务交互。
   - **示例**：一个 `User` 类，它包含属性如用户名、密码、电子邮件等，和一些方法如验证用户名是否有效等。

### 5. **Service**
   - **职责**：Service 主要负责执行应用程序的核心服务逻辑。它们可以用于访问远程数据、与后端 API 通信、处理业务流程等。Service 通常会处理更复杂的逻辑，并提供跨多个 ViewModel 使用的功能。
   - **特点**：
     - 可能包含与外部资源的交互，如数据库、API、文件系统等。
     - 可以定义为接口，以便于替换和测试。
     - 通常是单一职责的，负责某一方面的业务逻辑（例如，数据获取、数据处理、文件操作等）。
   - **示例**：一个 `UserService` 类，它可以处理用户登录、注册、密码重置等操作。

### 总结

- **ViewModel**：与视图交互的中介，负责显示数据和用户交互的逻辑。
- **Helper**：辅助工具类，封装常见、重复的操作。
- **Contracts**：接口或协议，规范不同部分之间的交互，支持松耦合和测试。
- **Model**：应用的数据模型和核心业务逻辑，负责数据和业务的存取与处理。
- **Service**：提供应用核心服务，如 API 调用、数据操作等。

这些组件共同作用，帮助 WinUI 3 应用保持清晰的职责分离，方便管理和扩展。

# 服务和依赖注入

## 概念

在 `App.xaml.cs` 中调用 `services.AddSingleton<IFileService, FileService>();` 进行服务注册后，您可以通过依赖注入 (Dependency Injection, DI) 来在应用程序中的其他部分使用 `IFileService` 服务。以下是如何在 WinUI 3 应用中正确使用已注册的服务的步骤。

### 步骤概述：
1. **注册服务**：在 `App.xaml.cs` 中注册服务。
2. **获取服务实例**：通过构造函数注入（或其他方式）在需要的地方获取服务实例。

### 1. **在 App.xaml.cs 中注册服务**

首先，确保在 `App.xaml.cs` 中正确注册服务。这是应用启动时的服务配置部分，通常会在 `OnLaunched` 方法中进行服务注册。

#### 示例：在 `App.xaml.cs` 中注册服务
```csharp
using Microsoft.Extensions.DependencyInjection;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using CalculatorApp.Services;  // 假设你有一个名为 Services 的文件夹

namespace CalculatorApp
{
    public partial class App : Application
    {
        // 服务容器
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            this.InitializeComponent();
            ConfigureServices();
        }

        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // 注册 FileService 服务
            serviceCollection.AddSingleton<IFileService, FileService>();

            // 其他服务可以继续注册
            // serviceCollection.AddSingleton<IOtherService, OtherService>();

            // 构建服务容器
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var window = new MainWindow();
            window.Activate();
        }
    }
}
```

在这个例子中，`IFileService` 被注册为一个单例服务，`FileService` 是它的实现。你可以在应用中的其他地方通过依赖注入来使用它。

### 2. **通过依赖注入在其他类中使用服务**

为了在其他地方使用已注册的服务，你可以通过依赖注入来获取服务的实例。最常见的做法是在构造函数中注入所需的服务。

#### 示例：在 ViewModel 中使用 `IFileService`

假设你在 `CalculatorApp` 中有一个 ViewModel（比如 `MainViewModel`），并希望使用 `IFileService`。

首先，在 `MainViewModel` 中声明 `IFileService`：

```csharp
using CalculatorApp.Services;

public class MainViewModel
{
    private readonly IFileService _fileService;

    // 通过构造函数注入 IFileService
    public MainViewModel(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void SaveData(string data)
    {
        // 使用 _fileService 保存数据
        _fileService.SaveToFile(data);
    }
}
```

#### 示例：在页面中获取 `MainViewModel` 并使用 `IFileService`

在页面中（例如 `MainPage.xaml.cs`）中，你可以通过依赖注入获取 `MainViewModel` 实例。为了使这个过程自动化，你通常会在构造函数中使用依赖注入。

```csharp
using Microsoft.Extensions.DependencyInjection;

public sealed partial class MainPage : Page
{
    private readonly MainViewModel _viewModel;

    public MainPage()
    {
        this.InitializeComponent();

        // 从 ServiceProvider 获取 MainViewModel 实例
        _viewModel = App.ServiceProvider.GetRequiredService<MainViewModel>();

        // 设置数据绑定（如果需要）
        this.DataContext = _viewModel;
    }

    // 示例：使用 SaveData 方法
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string dataToSave = "Hello, World!";
        _viewModel.SaveData(dataToSave);
    }
}
```

### 3. **通过 ServiceProvider 直接获取服务**

如果你不想通过构造函数注入服务，也可以在需要的地方直接使用 `ServiceProvider` 获取服务的实例。这种方式在某些场景下也很有用。

#### 示例：直接从 `ServiceProvider` 获取服务

```csharp
using CalculatorApp.Services;

public class SomeClass
{
    public void SomeMethod()
    {
        // 通过 ServiceProvider 获取 IFileService 实例
        var fileService = App.ServiceProvider.GetRequiredService<IFileService>();
        
        // 使用 fileService 执行操作
        fileService.SaveToFile("Some data");
    }
}
```

> **注意**：这种方式通常不推荐过度使用，因为它不够灵活，依赖注入的方式更有利于测试和维护。

### 4. **更新 ViewModel 使用构造函数注入**

如果你使用的是 **MVVM** 模式并且希望在 `ViewModel` 中注入服务，你可以在注册 `ViewModel` 时也进行注入。

#### 示例：注册 ViewModel 和服务

```csharp
private void ConfigureServices()
{
    var serviceCollection = new ServiceCollection();

    // 注册 FileService 服务
    serviceCollection.AddSingleton<IFileService, FileService>();

    // 注册 MainViewModel
    serviceCollection.AddSingleton<MainViewModel>();

    // 其他服务
    // serviceCollection.AddSingleton<OtherService, OtherService>();

    // 构建服务容器
    ServiceProvider = serviceCollection.BuildServiceProvider();
}
```

这样，你就可以直接通过构造函数注入 `IFileService` 和 `MainViewModel`，使得你的服务和 ViewModel 实例自动管理。

### 总结

1. **服务注册**：在 `App.xaml.cs` 中使用 `services.AddSingleton<IFileService, FileService>();` 注册服务。
2. **依赖注入**：通过构造函数注入在 `ViewModel` 中获取和使用服务。
3. **从 ServiceProvider 获取服务**：在需要的地方可以直接通过 `ServiceProvider` 获取服务实例。

这种方式将服务的生命周期和依赖关系交给了 .NET 的依赖注入容器来管理，帮助你解耦应用的各个部分，增加代码的可维护性和测试性。

## 与直接实例化的区别

在软件开发中，**使用服务（依赖注入）**和**直接实例化类**是两种常见的对象创建和管理方式。它们有不同的优点和适用场景，下面是两者的异同对比：

### 1. **创建方式**
- **使用服务（依赖注入）**：
  - 通过依赖注入框架（如 `IServiceCollection`、`IServiceProvider`）来管理对象的创建和生命周期。
  - 服务在容器中被注册后，可以通过构造函数注入或手动获取实例（例如，`ServiceProvider.GetRequiredService<>()`）来使用。
  
- **直接实例化类**：
  - 直接通过 `new` 操作符创建对象。例如，`var obj = new MyClass();`。
  - 类的生命周期由开发者手动管理（如果类需要多个实例或有复杂的初始化要求，开发者需要自行处理）。

### 2. **生命周期管理**
- **使用服务（依赖注入）**：
  - 服务的生命周期由依赖注入容器管理。可以通过 `AddSingleton`、`AddTransient`、`AddScoped` 等方法来控制对象的生命周期：
    - `Singleton`：创建一个全局唯一的实例，整个应用程序生命周期中只会有一个实例。
    - `Transient`：每次请求都会创建一个新的实例。
    - `Scoped`：在请求范围内共享同一个实例（通常用于Web请求范围内的服务）。
  - 无需手动管理对象销毁，容器会自动处理依赖的生命周期。

- **直接实例化类**：
  - 对象生命周期由开发者手动管理。对象在创建时由开发者控制，但如果对象是复杂的，或者需要释放资源（如文件句柄、数据库连接等），开发者必须手动处理清理工作。

### 3. **解耦性和可维护性**
- **使用服务（依赖注入）**：
  - **解耦**：类之间的依赖通过接口而不是具体实现来连接，促进了代码的松耦合。例如，`ClassA` 依赖于 `IFileService` 接口而不是 `FileService` 实现类。
  - **可测试性**：使用依赖注入时，通常可以通过模拟（mock）依赖项（例如，使用 Mock 库）来简化单元测试。
  - **可维护性**：当需要修改服务的实现时，通常只需要更改服务的注册，而不需要更改依赖该服务的类代码。

- **直接实例化类**：
  - **紧耦合**：如果直接在类内部创建依赖对象，这会导致高耦合，类变得难以独立于其他类进行测试和重构。例如，`ClassA` 如果直接通过 `new FileService()` 实例化了 `FileService`，就与其紧密耦合，无法轻松替换它。
  - **难以测试**：直接实例化的对象不容易模拟，在单元测试中不易替换为模拟对象（mock）进行测试。
  - **不利于维护**：当需要修改类的依赖关系时，可能需要大量的代码重构。

### 4. **灵活性**
- **使用服务（依赖注入）**：
  - **灵活性高**：通过依赖注入，你可以轻松地切换实现，甚至在运行时替换服务的实现。例如，你可以将生产环境中的 `FileService` 替换为测试环境中的模拟 `FileService`。
  - 适合大型应用，尤其是当应用有多个依赖关系时，依赖注入容器可以很好地管理服务的生命周期和依赖关系。

- **直接实例化类**：
  - **灵活性低**：对象的创建和使用是固定的，无法在运行时改变。例如，如果你在类中直接创建了 `FileService` 的实例，那么如果想要替换为其他类型的服务（如 `CloudFileService`），需要修改源代码。
  - 不适合复杂系统，尤其是当类之间的依赖关系多样且复杂时，直接实例化会使得代码变得难以维护和扩展。

### 5. **性能**
- **使用服务（依赖注入）**：
  - **可能有些开销**：依赖注入容器本身会带来一些性能开销，尤其是在容器初始化时（例如，解析依赖关系）。不过，这通常是微不足道的，特别是在现代应用中。
  - **惰性加载**：通过依赖注入，可以将服务的实例化推迟到真正需要它们时才进行，这样可以减少不必要的计算和内存消耗。

- **直接实例化类**：
  - **性能开销较小**：直接使用 `new` 创建对象通常是直接且高效的，因为不需要通过依赖注入框架进行额外的查找和构造。
  - 然而，直接实例化可能导致重复创建对象，尤其是对于昂贵的对象（如数据库连接等），可能会浪费资源。

### 6. **适用场景**
- **使用服务（依赖注入）**：
  - 适用于**大型应用**、**多层架构**、**分布式系统**，尤其是当服务和对象之间有复杂的依赖关系时。
  - 适合**需要高度可测试性**和**易于替换服务的场景**，例如在不同的环境中使用不同的服务实现。
  - 在Web应用（如ASP.NET）和桌面应用（如WinUI 3）中广泛使用。

- **直接实例化类**：
  - 适用于**简单、小型应用**，或者没有复杂依赖关系的场景。
  - 适合一些独立的功能模块，直接实例化并没有带来太大的管理问题时。

### 7. **总结对比表**

| 特性               | 使用服务（依赖注入）                       | 直接实例化类                   |
|--------------------|-----------------------------------------|--------------------------------|
| **创建方式**       | 通过 DI 容器提供，服务注册并管理实例       | 使用 `new` 操作符直接创建对象   |
| **生命周期管理**   | 由 DI 容器自动管理生命周期（Singleton、Transient、Scoped） | 由开发者手动管理生命周期       |
| **解耦性**         | 高，依赖通过接口注入，松耦合               | 低，直接在类中创建依赖对象     |
| **可测试性**       | 高，易于替换服务、进行单元测试           | 低，难以替换和模拟依赖对象     |
| **灵活性**         | 高，服务可以动态替换，运行时配置服务       | 低，必须在代码中固定依赖关系   |
| **性能**           | 可能有微小开销（依赖注入容器解析）        | 直接创建对象，性能开销小       |
| **适用场景**       | 大型应用、需要高度可测试、可替换服务的场景 | 小型应用、依赖关系简单的场景   |

### 总结
- **使用服务（依赖注入）** 提供了更高的解耦性、可维护性和可测试性，适用于更复杂的应用和大型项目。
- **直接实例化类** 在小型应用或没有复杂依赖关系的场景下使用时更为直接和高效，但会导致较低的灵活性和可维护性。

**在大多数现代应用中，依赖注入通常是推荐的做法**，因为它能够更好地支持可维护性、可扩展性和测试性。