using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Streams;

namespace NotePadSharp.Contracts.Services;
public interface IRichEditBoxService
{
    RichEditBox Editor { get; }

    void SetEditor(RichEditBox editor);

    void LoadRtfContent(IRandomAccessStream randAccStream);

    void SaveRtfContent(IRandomAccessStream randAccStream);

    void LoadFromMemory();

    void SaveToMemory();

    bool EditorContentEmpty();

    bool CanUndo { get; }

    bool CanRedo { get; }

    void Undo();

    void Redo();

    void Paste();

    void ClearNew();

    event EventHandler UndoRedoStateChanged;

    void PerformUndoRedoStatusChange();

    event EventHandler SelectionChanged;

    void OnSelectionChanged();

    bool IsUpdatingSelection
    {
        get;
        set;
    }
}
