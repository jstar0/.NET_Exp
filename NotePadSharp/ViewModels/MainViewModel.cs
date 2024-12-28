using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.Behaviors;
using NotePadSharp.Contracts.Services;

namespace NotePadSharp.ViewModels;

public partial class MainViewModel : ObservableRecipient
{

    public IRichEditBoxService RichEditBoxService
    {
        get;
    }

    public MainViewModel(IRichEditBoxService richEditBoxService)
    {
        RichEditBoxService = richEditBoxService;
    }

    public void SetEditor(Microsoft.UI.Xaml.Controls.RichEditBox editor)
    {
        RichEditBoxService.SetEditor(editor);
    }
}
