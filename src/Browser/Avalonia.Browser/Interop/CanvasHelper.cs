using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace Avalonia.Browser.Interop;

internal record GLInfo(int32 ContextId, uint32 FboId, int32 Stencils, int32 Samples, int32 Depth);

internal static partial class CanvasHelper
{
    [JSExport]
    public static Task OnSizeChanged(int32 topLevelId, double width, double height, double dpr)
    {
        if (BrowserWindowingPlatform.IsThreadingEnabled)
        {
            return Dispatcher.UIThread.InvokeAsync(() => BrowserTopLevelImpl
                    .TryGetTopLevel(topLevelId)?.Surface?.OnSizeChanged(width, height, dpr))
                .GetTask();
        }
        else
        {
            BrowserTopLevelImpl
                .TryGetTopLevel(topLevelId)?.Surface?.OnSizeChanged(width, height, dpr);
            return Task.CompletedTask;
        }
    }

    [JSImport("CanvasSurface.create", AvaloniaModule.MainModuleName)]
    public static partial JSObject CreateRenderTargetSurface(JSObject canvasSurface, int32[] modes, int32 topLevelId, int32 threadId);

    [JSImport("CanvasSurface.destroy", AvaloniaModule.MainModuleName)]
    public static partial void Destroy(JSObject canvasSurface);
}
