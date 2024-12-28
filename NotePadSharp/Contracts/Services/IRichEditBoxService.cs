using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Streams;

namespace NotePadSharp.Contracts.Services;
public interface IRichEditBoxService
{

    void SetEditor(RichEditBox editor);

    void LoadRtfContent(IRandomAccessStream randAccStream);

    void SaveRtfContent(IRandomAccessStream randAccStream);

    void LoadFromMemory();

    void SaveToMemory();
}
