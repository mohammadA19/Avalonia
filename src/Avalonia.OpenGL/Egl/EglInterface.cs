using System;
using System.Runtime.InteropServices;
using Avalonia.Compatibility;
using Avalonia.Platform;
using Avalonia.Platform.Interop;
using Avalonia.SourceGenerator;

namespace Avalonia.OpenGL.Egl
{
    public unsafe partial class EglInterface
    {
        public EglInterface(Func<string, IntPtr> getProcAddress)
        {
            Initialize(getProcAddress);
        }
        
        public EglInterface(string library) : this(Load(library))
        {
        }

        public EglInterface() : this(Load())
        {
            
        }

        static Func<string, IntPtr> Load()
        {
            if(OperatingSystemEx.IsLinux())
                return Load("libEGL.so.1");
            if (OperatingSystemEx.IsAndroid())
                return Load("libEGL.so");

            throw new PlatformNotSupportedException();
        }

        static Func<string, IntPtr> Load(string library)
        {
            var lib = NativeLibraryEx.Load(library);
            return (s) => NativeLibraryEx.TryGetExport(lib, s, out var address) ? address : default;
        }

        // ReSharper disable UnassignedGetOnlyAutoProperty
        
        [GetProcAddress("eglGetError")]
        public partial int32 GetError();
        
        [GetProcAddress("eglGetDisplay")]
        public partial IntPtr GetDisplay(IntPtr nativeDisplay);
        
        [GetProcAddress("eglGetPlatformDisplayEXT", true)]
        public partial IntPtr GetPlatformDisplayExt(int32 platform, IntPtr nativeDisplay, int32[]? attrs);

        [GetProcAddress("eglInitialize")]        
        public partial bool Initialize(IntPtr display, out int32 major, out int32 minor);
        
        [GetProcAddress("eglTerminate")]        
        public partial void Terminate(IntPtr display);

        [GetProcAddress("eglGetProcAddress")]        
        public partial IntPtr GetProcAddress(IntPtr proc);

        [GetProcAddress("eglBindAPI")]
        public partial bool BindApi(int32 api);

        [GetProcAddress("eglChooseConfig")]
        public partial bool ChooseConfig(IntPtr display, int32[] attribs,
            out IntPtr surfaceConfig, int32 numConfigs, out int32 choosenConfig);
        
        [GetProcAddress("eglCreateContext")]
        public partial IntPtr CreateContext(IntPtr display, IntPtr config,
            IntPtr share, int32[] attrs);
        
        [GetProcAddress("eglDestroyContext")]
        public partial bool DestroyContext(IntPtr display, IntPtr context);
        
        [GetProcAddress("eglCreatePbufferSurface")]
        public partial IntPtr CreatePBufferSurface(IntPtr display, IntPtr config, int32[]? attrs);

        [GetProcAddress("eglMakeCurrent")]
        public partial bool MakeCurrent(IntPtr display, IntPtr draw, IntPtr read, IntPtr context);
        
        [GetProcAddress("eglGetCurrentContext")]
        public partial IntPtr GetCurrentContext();

        [GetProcAddress("eglGetCurrentDisplay")]
        public partial IntPtr GetCurrentDisplay();
        
        [GetProcAddress("eglGetCurrentSurface")] 
        public partial IntPtr GetCurrentSurface(int32 readDraw);

        [GetProcAddress("eglDestroySurface")]
        public partial void DestroySurface(IntPtr display, IntPtr surface);

        [GetProcAddress("eglSwapBuffers")]
        public partial void SwapBuffers(IntPtr display, IntPtr surface);

        [GetProcAddress("eglCreateWindowSurface")]
        public partial IntPtr CreateWindowSurface(IntPtr display, IntPtr config, IntPtr window, int32[]? attrs);

        [GetProcAddress("eglBindTexImage")]
        public partial int32 BindTexImage(IntPtr display, IntPtr surface, int32 buffer);

        [GetProcAddress("eglGetConfigAttrib")]
        public partial bool GetConfigAttrib(IntPtr display, IntPtr config, int32 attr, out int32 rv);
        
        [GetProcAddress("eglWaitGL")]
        public partial bool WaitGL();
        
        [GetProcAddress("eglWaitClient")]
        public partial bool WaitClient();
        
        [GetProcAddress("eglWaitNative")]
        public partial bool WaitNative(int32 engine);
        
        [GetProcAddress("eglQueryString")]
        public partial IntPtr QueryStringNative(IntPtr display, int32 i);
        
        public string? QueryString(IntPtr display, int32 i)
        {
            var rv = QueryStringNative(display, i);
            if (rv == IntPtr.Zero)
                return null;
            return Marshal.PtrToStringAnsi(rv);
        }

        [GetProcAddress("eglCreatePbufferFromClientBuffer")]
        public partial IntPtr CreatePbufferFromClientBuffer(IntPtr display, int32 buftype, IntPtr buffer, IntPtr config, int32[]? attrib_list);

        [GetProcAddress("eglCreatePbufferFromClientBuffer")]
        public partial IntPtr CreatePbufferFromClientBufferPtr(IntPtr display, int32 buftype, IntPtr buffer, IntPtr config, int32* attrib_list);

        [GetProcAddress("eglQueryDisplayAttribEXT", true)]
        public partial bool QueryDisplayAttribExt(IntPtr display, int32 attr, out IntPtr res);

        
        [GetProcAddress("eglQueryDeviceAttribEXT", true)]
        public partial bool QueryDeviceAttribExt(IntPtr display, int32 attr, out IntPtr res);
    }
}
