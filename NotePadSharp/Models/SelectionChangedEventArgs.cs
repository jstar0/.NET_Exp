using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI.Windows.UI;

namespace NotePadSharp.Models;

public class SelectionChangedEventArgs
{
    public SelectionChangedEventArgs() { }

    public bool IsBold
    {
        get;
        set;
    } = false;

    public bool IsItalic
    {
        get;
        set;
    } = false;

    public bool IsUnderline
    {
        get;
        set;
    } = false;

    public bool IsStrikethrough
    {
        get;
        set;
    } = false;

    public AlignmentType.Type AlignmentType
    {
        get;
        set;
    } = Models.AlignmentType.Type.Left;

    public string FontFamily
    {
        get;
        set;
    } = "Segoe UI";

    public Windows.UI.Color ForegroundColor
    {
        get;
        set;
    } = Windows.UI.Color.FromArgb(255, 0, 0, 0);
}
