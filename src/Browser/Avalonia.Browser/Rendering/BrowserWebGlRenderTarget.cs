using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using Avalonia.Browser.Interop;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Platform;
using Avalonia.Reactive;

namespace Avalonia.Browser.Rendering;

partial class BrowserWebGlRenderTarget : BrowserRenderTarget, IGlPlatformSurface
{
    private readonly Func<(PixelSize Size, double Scaling)> _sizeGetter;
    private readonly GLInfo _glInfo;
    public IGlContext GlContext { get; }

    public BrowserWebGlRenderTarget(JSObject js, Func<(PixelSize, double)> sizeGetter) : base(js)
    {
        _sizeGetter = sizeGetter;
        _glInfo = new GLInfo(
            js.GetPropertyAsInt32("contextHandle")!,
            (uint32)js.GetPropertyAsInt32("fboId"),
            js.GetPropertyAsInt32("stencil"),
            js.GetPropertyAsInt32("sample"),
            js.GetPropertyAsInt32("depth"));
        var contextId = js.GetPropertyAsInt32("contextHandle");
        var version = js.GetPropertyAsJSObject("attrs")!.GetPropertyAsInt32("majorVersion");
        GlContext = new WebGlContext(contextId, new GlVersion(GlProfileType.OpenGLES, version > 1 ? 3 : 2, 0),
            _glInfo.Samples, _glInfo.Stencils);
    }
    
    class GlSession : IGlPlatformSurfaceRenderingSession
    {
        private IDisposable? _restoreContext;

        public GlSession(IGlContext context, PixelSize size, double scaling, IDisposable restoreContext)
        {
            _restoreContext = restoreContext;
            Context = context;
            Size = size;
            Scaling = scaling;
        }

        public void Dispose()
        {
            _restoreContext?.Dispose();
            _restoreContext = null;
        }

        public IGlContext Context { get; }
        public PixelSize Size { get; }
        // This should technically be delivered via CompositionTarget.Scaling anyway, why do we still have this property
        public double Scaling { get; }
        public bool IsYFlipped => false;
    }
    
    class GlSurface : IGlPlatformSurfaceRenderTarget
    {
        private readonly BrowserWebGlRenderTarget _target;

        public GlSurface(BrowserWebGlRenderTarget target)
        {
            _target = target;
        }
        
        public void Dispose()
        {
            // No-op
        }

        public IGlPlatformSurfaceRenderingSession BeginDraw()
        {
            var s = _target._sizeGetter();
            _target.UpdateSize(s.Size);
            var restoreContext = _target.GlContext.EnsureCurrent();
            _target.GlContext.GlInterface.BindFramebuffer(GlConsts.GL_FRAMEBUFFER, (int32)_target._glInfo.FboId);
            return new GlSession(_target.GlContext, s.Size, s.Scaling, restoreContext);
        }
    }

    public override IPlatformGraphicsContext? PlatformGraphicsContext => GlContext;
    public IGlPlatformSurfaceRenderTarget CreateGlRenderTarget(IGlContext context)
    {
        return new GlSurface(this);
    }
}

partial class WebGlContext : IGlContext, Avalonia.Skia.IGlSkiaSpecificOptionsFeature
{
    [JSImport("WebGlRenderTarget.getCurrentContext", AvaloniaModule.MainModuleName)]
    private static partial int32 GetCurrentContext();

    [JSImport("WebGlRenderTarget.makeContextCurrent", AvaloniaModule.MainModuleName)]
    private static partial bool MakeContextCurrent(int32 context);

    [LibraryImport("libSkiaSharp", EntryPoint = "eglGetProcAddress", StringMarshalling = StringMarshalling.Utf8)]
    private static partial IntPtr eglGetProcAddress(string name);

    private int32 _contextId;
    private readonly Thread _thread;

    public WebGlContext(int32 contextId, GlVersion version, int32 sampleCount, int32 stencilSize)
    {
        Version = version;
        SampleCount = sampleCount;
        StencilSize = stencilSize;
        _contextId = contextId;
        _thread = Thread.CurrentThread;

        using (MakeCurrent())
            GlInterface = new GlInterface(version, eglGetProcAddress);
    }

    void VerifyAccess()
    {
        if (_thread != Thread.CurrentThread)
            throw new InvalidOperationException("Call from invalid thread");
    }
    
    public IDisposable EnsureCurrent()
    {
        VerifyAccess();
        if(GetCurrentContext() == _contextId)
            return Disposable.Empty;
        return MakeCurrent();
    }

    class RestoreContext : IDisposable
    {
        private int32? _contextId;

        public RestoreContext(int32 contextId)
        {
            _contextId = contextId;
        }

        public void Dispose()
        {
            if (_contextId != null)
                MakeContextCurrent(_contextId.Value);
            _contextId = null;
        }
    }
    
    public IDisposable MakeCurrent()
    {
        VerifyAccess();
        var old = GetCurrentContext();
        if (!MakeContextCurrent(_contextId))
            throw new OpenGlException("Unable to make the context current");
        return new RestoreContext(old);
    }
    
    public void Dispose()
    {
        // No-op, destroyed with the render target
    }

    public object? TryGetFeature(Type featureType) => null;

    // TODO: Implement
    public bool IsLost => false;
    public GlVersion Version { get; }
    public GlInterface GlInterface { get; }
    public int32 SampleCount { get; }
    public int32 StencilSize { get; }


    public bool IsSharedWith(IGlContext context) => false;

    public bool CanCreateSharedContext => false;

    public IGlContext? CreateSharedContext(IEnumerable<GlVersion>? preferredVersions = null) =>
        throw new NotSupportedException();

    public bool UseNativeSkiaGrGlInterface => true;
}
