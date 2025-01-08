using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Controls;
using NotePadSharp.Contracts.Services;
using System.Reflection.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.Behaviors;
using Microsoft.UI.Text;

namespace NotePadSharp.Services;
public class RichEditBoxService(IMainNotificationService mainNotificationService) : IRichEditBoxService
{
    // 存储 editor 的引用
    private RichEditBox? _editor;

    private string? _richTextContent;

    public bool CanUndo => _editor != null && _editor.Document.CanUndo();

    public bool CanRedo => _editor != null && _editor.Document.CanRedo();

    public RichEditBox Editor
    {
        get => _editor;
    }

    public void SetEditor(RichEditBox editor)
    {
        _editor = editor;
        LoadFromMemory();
    }

    public void LoadRtfContent(IRandomAccessStream randAccStream)
    {
        _editor.Document.LoadFromStream(Microsoft.UI.Text.TextSetOptions.FormatRtf, randAccStream);
    }

    public void SaveRtfContent(IRandomAccessStream randAccStream)
    {
        _editor.Document.SaveToStream(Microsoft.UI.Text.TextGetOptions.FormatRtf, randAccStream);
    }

    public async void LoadFromMemory()
    {
        try
        {
            if (string.IsNullOrEmpty(_richTextContent))
            {
                return;
            }

            // 将 RTF 内容加载回 RichEditBox
            using var memoryStream = new InMemoryRandomAccessStream();
            // 将字符串写入流
            using (var dataWriter = new DataWriter(memoryStream.GetOutputStreamAt(0)))
            {
                dataWriter.WriteString(_richTextContent);
                await dataWriter.StoreAsync();
                await dataWriter.FlushAsync();
            }
            // 将流中的内容加载到 Document 中，使用 RTF 格式
            _editor.Document.LoadFromStream(TextSetOptions.FormatRtf, memoryStream);

            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"已从内存加载暂存的数据",
                Severity = InfoBarSeverity.Success,
                Duration = TimeSpan.FromSeconds(2)
            };

            mainNotificationService.ClearNotificationQueue();
            mainNotificationService.ShowNotification(notification);
        }
        catch (Exception e)
        {
            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"无法从内存加载暂存的数据。\n错误：{e.Message}",
                Severity = InfoBarSeverity.Error,
                Duration = TimeSpan.FromSeconds(2)
            };

            mainNotificationService.ClearNotificationQueue();
            mainNotificationService.ShowNotification(notification);
        }
    }

    public async void SaveToMemory()
    {
        try
        {
            using var memoryStream = new InMemoryRandomAccessStream();
            // 将 Document 保存到流中，使用 RTF 格式
            _editor.Document.SaveToStream(TextGetOptions.FormatRtf, memoryStream);

            // 读取流中的内容并转换为字符串
            using var dataReader = new DataReader(memoryStream.GetInputStreamAt(0));
            uint size = (uint)memoryStream.Size;
            await dataReader.LoadAsync(size);
            _richTextContent = dataReader.ReadString(size);

            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"已暂存数据至内存",
                Severity = InfoBarSeverity.Success,
                Duration = TimeSpan.FromSeconds(2)
            };

            mainNotificationService.ClearNotificationQueue();
            mainNotificationService.ShowNotification(notification);
        }
        catch (Exception e)
        {
            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"无法暂存数据至内存。\n错误：{e.Message}",
                Severity = InfoBarSeverity.Error,
                Duration = TimeSpan.FromSeconds(2)
            };

            mainNotificationService.ClearNotificationQueue();
            mainNotificationService.ShowNotification(notification);
        }
    }

    public bool EditorContentEmpty()
    {
        if (_editor == null)
        {
            return true;
        }

        return !_editor.Document.CanUndo();
    }

    public void Undo()
    {
        if (_editor != null)
        {
            _editor.Document.Undo();
        }
    }

    public void Redo()
    {
        if (_editor != null)
        {
            _editor.Document.Redo();
        }
    }

    public void Paste()
    {
        if (_editor != null)
        {
            _editor.Document.Selection.Paste(0);
        }
    }

    public void ClearNew()
    {
        _richTextContent = null;

        if (_editor != null)
        {
            _editor.Document.SetText(TextSetOptions.None, string.Empty);
        }

        mainNotificationService.ClearNotificationQueue();
    }

    public event EventHandler UndoRedoStateChanged;

    public virtual void PerformUndoRedoStatusChange()
    {
        UndoRedoStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler SelectionChanged;

    public virtual void OnSelectionChanged()
    {
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpdatingSelection
    {
        get;
        set;
    } = false;
}
