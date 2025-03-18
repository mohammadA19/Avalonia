using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace Avalonia.Browser.Interop;

internal static partial class InputHelper
{
    public static Task RedirectInputAsync(int32 topLevelId, Action<BrowserTopLevelImpl> handler)
    {
        if (BrowserTopLevelImpl.TryGetTopLevel(topLevelId) is { } topLevelImpl) handler(topLevelImpl);
        return Task.CompletedTask;
    }

    public static Task<T> RedirectInputRetunAsync<T>(int32 topLevelId, Func<BrowserTopLevelImpl,T> handler, T @default)
    {
        if (BrowserTopLevelImpl.TryGetTopLevel(topLevelId) is { } topLevelImpl)
            return Task.FromResult(handler(topLevelImpl));
        return Task.FromResult(@default);
    }

    [JSImport("InputHelper.subscribeInputEvents", AvaloniaModule.MainModuleName)]
    public static partial void SubscribeInputEvents(JSObject htmlElement, int32 topLevelId);

    [JSExport]
    public static Task<bool> OnKeyDown(int32 topLevelId, string code, string key, int32 modifier) =>
        RedirectInputRetunAsync(topLevelId, t => t.InputHandler.OnKeyDown(code, key, modifier), false);

    [JSExport]
    public static Task<bool> OnKeyUp(int32 topLevelId, string code, string key, int32 modifier) =>
        RedirectInputRetunAsync(topLevelId, t => t.InputHandler.OnKeyUp(code, key, modifier), false);

    [JSExport]
    public static Task OnBeforeInput(int32 topLevelId, string inputType, int32 start, int32 end) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.TextInputMethod.OnBeforeInput(inputType, start, end));

    [JSExport]
    public static Task OnCompositionStart(int32 topLevelId) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.TextInputMethod.OnCompositionStart());

    [JSExport]
    public static Task OnCompositionUpdate(int32 topLevelId, string? data) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.TextInputMethod.OnCompositionUpdate(data));

    [JSExport]
    public static Task OnCompositionEnd(int32 topLevelId, string? data) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.TextInputMethod.OnCompositionEnd(data));

    [JSExport]
    public static Task OnPointerMove(int32 topLevelId, string pointerType, [JSMarshalAs<JSType.Number>] int64 pointerId,
        double offsetX, double offsetY, double pressure, double tiltX, double tiltY, double twist, int32 modifier, JSObject argsObj) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler
            .OnPointerMove(pointerType, pointerId, offsetX, offsetY, pressure, tiltX, tiltY, twist, modifier, argsObj));

    [JSExport]
    public static Task OnPointerDown(int32 topLevelId, string pointerType, [JSMarshalAs<JSType.Number>] int64 pointerId, int32 buttons,
        double offsetX, double offsetY, double pressure, double tiltX, double tiltY, double twist, int32 modifier) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler
            .OnPointerDown(pointerType, pointerId, buttons, offsetX, offsetY, pressure, tiltX, tiltY, twist, modifier));

    [JSExport]
    public static Task OnPointerUp(int32 topLevelId, string pointerType, [JSMarshalAs<JSType.Number>] int64 pointerId, int32 buttons,
        double offsetX, double offsetY, double pressure, double tiltX, double tiltY, double twist, int32 modifier) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler
            .OnPointerUp(pointerType, pointerId, buttons, offsetX, offsetY, pressure, tiltX, tiltY, twist, modifier));

    [JSExport]
    public static Task OnPointerCancel(int32 topLevelId, string pointerType, [JSMarshalAs<JSType.Number>] int64 pointerId,
        double offsetX, double offsetY, double pressure, double tiltX, double tiltY, double twist, int32 modifier) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler
            .OnPointerCancel(pointerType, pointerId, offsetX, offsetY, pressure, tiltX, tiltY, twist, modifier));

    [JSExport]
    public static Task OnWheel(int32 topLevelId,
        double offsetX, double offsetY,
        double deltaX, double deltaY, int32 modifier) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.OnWheel(offsetX, offsetY, deltaX, deltaY, modifier));

    [JSExport]
    public static Task OnDragDrop(int32 topLevelId, string type, double offsetX, double offsetY, int32 modifiers, string? effectAllowedStr, JSObject? dataTransfer) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.OnDragEvent(type, offsetX, offsetY, modifiers, effectAllowedStr, dataTransfer));

    [JSExport]
    public static Task OnKeyboardGeometryChange(int32 topLevelId, double x, double y, double width, double height) =>
        RedirectInputAsync(topLevelId, t => t.InputHandler.InputPane
            .OnGeometryChange(x, y, width, height));

    [JSImport("InputHelper.getCoalescedEvents", AvaloniaModule.MainModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial double[] GetCoalescedEvents(JSObject pointerEvent);

    [JSImport("InputHelper.clearInput", AvaloniaModule.MainModuleName)]
    public static partial void ClearInputElement(JSObject htmlElement);

    [JSImport("InputHelper.focusElement", AvaloniaModule.MainModuleName)]
    public static partial void FocusElement(JSObject htmlElement);

    [JSImport("InputHelper.setCursor", AvaloniaModule.MainModuleName)]
    public static partial void SetCursor(JSObject htmlElement, string kind);

    [JSImport("InputHelper.hide", AvaloniaModule.MainModuleName)]
    public static partial void HideElement(JSObject htmlElement);

    [JSImport("InputHelper.show", AvaloniaModule.MainModuleName)]
    public static partial void ShowElement(JSObject htmlElement);

    [JSImport("InputHelper.setSurroundingText", AvaloniaModule.MainModuleName)]
    public static partial void SetSurroundingText(JSObject htmlElement, string text, int32 start, int32 end);

    [JSImport("InputHelper.setBounds", AvaloniaModule.MainModuleName)]
    public static partial void SetBounds(JSObject htmlElement, int32 x, int32 y, int32 width, int32 height, int32 caret);

    [JSImport("InputHelper.initializeBackgroundHandlers", AvaloniaModule.MainModuleName)]
    public static partial void InitializeBackgroundHandlers(JSObject globalThis);

    [JSImport("InputHelper.readClipboardText", AvaloniaModule.MainModuleName)]
    public static partial Task<string> ReadClipboardTextAsync(JSObject globalThis);

    [JSImport("InputHelper.writeClipboardText", AvaloniaModule.MainModuleName)]
    public static partial Task WriteClipboardTextAsync(JSObject globalThis, string text);

    [JSImport("InputHelper.setPointerCapture", AvaloniaModule.MainModuleName)]
    public static partial void
        SetPointerCapture(JSObject containerElement, [JSMarshalAs<JSType.Number>] int64 pointerId);

    [JSImport("InputHelper.releasePointerCapture", AvaloniaModule.MainModuleName)]
    public static partial void ReleasePointerCapture(JSObject containerElement,
        [JSMarshalAs<JSType.Number>] int64 pointerId);
}
