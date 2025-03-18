using System;
using Avalonia.Utilities;
using Avalonia.Win32.Interop;

namespace Avalonia.Win32;

internal class NonPumpingWaitHelperImpl : NonPumpingLockHelper.IHelperImpl
{
    public static NonPumpingWaitHelperImpl Instance { get; } = new();
    public int32 Wait(IntPtr[] waitHandles, bool waitAll, int32 millisecondsTimeout) =>
        UnmanagedMethods.WaitForMultipleObjectsEx(waitHandles.Length, waitHandles, waitAll,
            millisecondsTimeout, false);
}