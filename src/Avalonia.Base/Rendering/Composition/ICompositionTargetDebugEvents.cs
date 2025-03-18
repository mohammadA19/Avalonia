namespace Avalonia.Rendering.Composition;

internal interface ICompositionTargetDebugEvents
{
    int32 RenderedVisuals { get; }
    void IncrementRenderedVisuals();
    void RectInvalidated(Rect rc);
}
