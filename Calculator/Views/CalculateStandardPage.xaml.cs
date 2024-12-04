using System.Collections.Specialized;
using Calculator.Behaviors;
using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.System;

namespace Calculator.Views;

public sealed partial class CalculateStandardPage : Page
{
    public double NumpadButtonFontSize
    {
        get => (double)GetValue(NumpadButtonFontSizeProperty);
        set => SetValue(NumpadButtonFontSizeProperty, value);
    }

    public static readonly DependencyProperty NumpadButtonFontSizeProperty =
        DependencyProperty.Register(
            nameof(NumpadButtonFontSize),
            typeof(double),
            typeof(CalculateStandardPage),
            new PropertyMetadata(16.0));

    public double MemoryButtonFontSize
    {
        get => (double)GetValue(MemoryButtonFontSizeProperty);
        set => SetValue(MemoryButtonFontSizeProperty, value);
    }

    public static readonly DependencyProperty MemoryButtonFontSizeProperty =
        DependencyProperty.Register(
            nameof(MemoryButtonFontSize),
            typeof(double),
            typeof(CalculateStandardPage),
            new PropertyMetadata(10.0));

    public CalculateStandardViewModel ViewModel
    {
        get;
    }

    private int _previousSelectedIndex = 0;

    private void UpdateMemoryButtonEnabling(object? sender, NotifyCollectionChangedEventArgs? e)
    {
        if (ViewModel.Memory.Count == 0)
        {
            ButtonMc.IsEnabled = false;
            ButtonMr.IsEnabled = false;
        }
        else
        {
            ButtonMc.IsEnabled = true;
            ButtonMr.IsEnabled = true;
        }
    }

    public CalculateStandardPage()
    {
        ViewModel = App.GetService<CalculateStandardViewModel>();
        InitializeComponent();
        DataContext = ViewModel;
        ViewModel.Memory.CollectionChanged += UpdateMemoryButtonEnabling;
        UpdateMemoryButtonEnabling(null, null);

        // 本页禁用标题
        NavigationViewHeaderBehavior.SetHeaderMode(this, NavigationViewHeaderMode.Never);
    }

    private void ToggleRightContentButton_Click(object sender, RoutedEventArgs e)
    {
        App.MainWindow.Width += 400;
    }

    private void SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        var selectedItem = sender.SelectedItem;
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);

        var pageType = currentSelectedIndex switch
        {
            0 => typeof(StandardHistoryMemory.HistoryPage),
            _ => typeof(StandardHistoryMemory.MemoryPage)
        };

        var slideNavigationTransitionEffect = currentSelectedIndex - _previousSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

        HistoryMemoryFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });

        _previousSelectedIndex = currentSelectedIndex;
    }

    private void OnButtonInvokedExecArgsNull(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
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

    private void OnTextBlockCopyInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (ViewModel.TextBlockCopy.CanExecute(null))
        {
            ViewModel.TextBlockCopy.Execute(null);
        }
        args.Handled = true;
    }

    private void OnTextBlockPasteInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (ViewModel.TextBlockPaste.CanExecute(null))
        {
            ViewModel.TextBlockPaste.Execute(null);
        }
        args.Handled = true;
    }
}
