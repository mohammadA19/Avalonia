using Avalonia.Input;
using static Avalonia.Win32.Interop.UnmanagedMethods;
using static Avalonia.Win32.Interop.UnmanagedMethods.VirtualKeyStates;

namespace Avalonia.Win32.Input
{
    internal sealed class WindowsKeyboardDevice : KeyboardDevice
    {
        public new static WindowsKeyboardDevice Instance { get; } = new();

        public unsafe RawInputModifiers Modifiers
        {
            get
            {
                fixed (uint8* keyStates = stackalloc uint8[256])
                {
                    GetKeyboardState(keyStates);

                    var result = RawInputModifiers.None;

                    if (((keyStates[(int32)VK_LMENU] | keyStates[(int32)VK_RMENU]) & 0x80) != 0)
                    {
                        result |= RawInputModifiers.Alt;
                    }

                    if (((keyStates[(int32)VK_LCONTROL] | keyStates[(int32)VK_RCONTROL]) & 0x80) != 0)
                    {
                        result |= RawInputModifiers.Control;
                    }

                    if (((keyStates[(int32)VK_LSHIFT] | keyStates[(int32)VK_RSHIFT]) & 0x80) != 0)
                    {
                        result |= RawInputModifiers.Shift;
                    }

                    if (((keyStates[(int32)VK_LWIN] | keyStates[(int32)VK_RWIN]) & 0x80) != 0)
                    {
                        result |= RawInputModifiers.Meta;
                    }

                    return result;
                }
            }
        }

        private WindowsKeyboardDevice()
        {
        }
    }
}
