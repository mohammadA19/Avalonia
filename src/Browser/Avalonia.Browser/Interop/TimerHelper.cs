using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace Avalonia.Browser.Interop;

internal static partial class TimerHelper
{
    [JSImport("TimerHelper.runAnimationFrames", AvaloniaModule.MainModuleName)]
    public static partial void RunAnimationFrames();

    public static Action<double>? AnimationFrame;
    [JSExport]
    public static void JsExportOnAnimationFrame(double d)
    {
        AnimationFrame?.Invoke(d);
    }
    
    public static Action? Timeout;
    [JSExport]
    public static Task JsExportOnTimeout()
    {
        Timeout?.Invoke();
        return Task.CompletedTask;
    }

    [JSImport("TimerHelper.setTimeout", AvaloniaModule.MainModuleName)]
    public static partial int32 SetTimeout(int32 intervalMs);

    [JSImport("globalThis.clearTimeout")]
    public static partial int32 ClearTimeout(int32 id);

    public static Action? Interval;
    [JSExport]
    public static Task JsExportOnInterval()
    {
        Interval?.Invoke();
        return Task.CompletedTask;
    }
    
    [JSImport("TimerHelper.setInterval", AvaloniaModule.MainModuleName)]
    public static partial int32 SetInterval( int32 intervalMs);

    [JSImport("globalThis.clearInterval")]
    public static partial int32 ClearInterval(int32 id);
}
