using System;
using System.Runtime.InteropServices;

namespace ControlCatalog.NetCore;

internal unsafe class WinApi
{
    public enum CommonControls : uint32
    {
        ICC_LISTVIEW_CLASSES = 0x00000001, // listview, header
        ICC_TREEVIEW_CLASSES = 0x00000002, // treeview, tooltips
        ICC_BAR_CLASSES = 0x00000004, // toolbar, statusbar, trackbar, tooltips
        ICC_TAB_CLASSES = 0x00000008, // tab, tooltips
        ICC_UPDOWN_CLASS = 0x00000010, // updown
        ICC_PROGRESS_CLASS = 0x00000020, // progress
        ICC_HOTKEY_CLASS = 0x00000040, // hotkey
        ICC_ANIMATE_CLASS = 0x00000080, // animate
        ICC_WIN95_CLASSES = 0x000000FF,
        ICC_DATE_CLASSES = 0x00000100, // month picker, date picker, time picker, updown
        ICC_USEREX_CLASSES = 0x00000200, // comboex
        ICC_COOL_CLASSES = 0x00000400, // rebar (coolbar) control
        ICC_INTERNET_CLASSES = 0x00000800,
        ICC_PAGESCROLLER_CLASS = 0x00001000, // page scroller
        ICC_NATIVEFNTCTL_CLASS = 0x00002000, // native font control
        ICC_STANDARD_CLASSES = 0x00004000,
        ICC_LINK_CLASS = 0x00008000
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INITCOMMONCONTROLSEX
    {
        public int32 dwSize;
        public uint32 dwICC;
    }

    [DllImport("Comctl32.dll")]
    public static extern void InitCommonControlsEx(ref INITCOMMONCONTROLSEX init);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool DestroyWindow(IntPtr hwnd);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW", ExactSpelling = true)]
    public static extern IntPtr LoadLibrary(string lib);


    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr CreateWindowEx(
        int32 dwExStyle,
        string lpClassName,
        string lpWindowName,
        uint32 dwStyle,
        int32 x,
        int32 y,
        int32 nWidth,
        int32 nHeight,
        IntPtr hWndParent,
        IntPtr hMenu,
        IntPtr hInstance,
        IntPtr lpParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct SETTEXTEX
    {
        public uint32 Flags;
        public uint32 Codepage;
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageW")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int32 Msg, ref SETTEXTEX wParam, uint8[] lParam);
}
