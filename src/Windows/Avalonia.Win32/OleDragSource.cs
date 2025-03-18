using System.Linq;

using Avalonia.MicroCom;
using Avalonia.Win32.Interop;
using Avalonia.Win32.Win32Com;

namespace Avalonia.Win32
{
    internal class OleDragSource : CallbackBase, IDropSource
    {
        private const int32 DRAGDROP_S_USEDEFAULTCURSORS = 0x00040102;
        private const int32 DRAGDROP_S_DROP = 0x00040100;
        private const int32 DRAGDROP_S_CANCEL = 0x00040101;

        private static readonly int32[] MOUSE_BUTTONS = new int32[] {
            (int32)UnmanagedMethods.ModifierKeys.MK_LBUTTON,
            (int32)UnmanagedMethods.ModifierKeys.MK_MBUTTON,
            (int32)UnmanagedMethods.ModifierKeys.MK_RBUTTON
        };

        public int32 QueryContinueDrag(int32 fEscapePressed, int32 grfKeyState)
        {
            if (fEscapePressed != 0)
                return DRAGDROP_S_CANCEL;

            int32 pressedMouseButtons = MOUSE_BUTTONS.Where(mb => (grfKeyState & mb) == mb).Count();

            if (pressedMouseButtons >= 2)
                return DRAGDROP_S_CANCEL;
            if (pressedMouseButtons == 0)
                return DRAGDROP_S_DROP;

            return unchecked((int32)UnmanagedMethods.HRESULT.S_OK);
        }

        public int32 GiveFeedback(DropEffect dwEffect)
        {
            return DRAGDROP_S_USEDEFAULTCURSORS;
        }
    }
}
