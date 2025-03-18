using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Avalonia.Platform.Interop;
using Avalonia.SourceGenerator;
using static Avalonia.OpenGL.GlConsts;

namespace Avalonia.OpenGL
{
    public unsafe partial class GlInterface : GlBasicInfoInterface
    {
        private readonly Func<string, IntPtr> _getProcAddress;
        public string? Version { get; }
        public string? Vendor { get; }
        public string? Renderer { get; }
        public GlContextInfo ContextInfo { get; }

        public class GlContextInfo
        {
            public GlVersion Version { get; }
            public HashSet<string> Extensions { get; }

            public GlContextInfo(GlVersion version, HashSet<string> extensions)
            {
                Version = version;
                Extensions = extensions;
            }

            public static GlContextInfo Create(GlVersion version, Func<string, IntPtr> getProcAddress)
            {
                var basicInfoInterface = new GlBasicInfoInterface(getProcAddress);
                var exts = basicInfoInterface.GetExtensions();
                return new GlContextInfo(version, new HashSet<string>(exts));
            }
        }

        private GlInterface(GlContextInfo info, Func<string, IntPtr> getProcAddress) : base(getProcAddress)
        {
            _getProcAddress = getProcAddress;
            ContextInfo = info;
            Version = GetString(GL_VERSION);
            Renderer = GetString(GL_RENDERER);
            Vendor = GetString(GL_VENDOR);
            Initialize(getProcAddress, ContextInfo);
        }

        public GlInterface(GlVersion version, Func<string, IntPtr> getProcAddress) : this(
            GlContextInfo.Create(version, getProcAddress), getProcAddress)
        {
        }

        public IntPtr GetProcAddress(string proc) => _getProcAddress(proc);

        [GetProcAddress("glClearStencil")]
        public partial void ClearStencil(int32 s);

        [GetProcAddress("glClearColor")]
        public partial void ClearColor(float r, float g, float b, float a);
        
        [GetProcAddress("glClearDepth", true)]
        internal partial void ClearDepthDouble(double value);
        
        [GetProcAddress("glClearDepthf", true)]
        internal partial void ClearDepthFloat(float value);

        public void ClearDepth(float value)
        {
            if(IsClearDepthDoubleAvailable)
                ClearDepthDouble(value);
            else if (IsClearDepthFloatAvailable)
                _addr_ClearDepthFloat(value);
        }
        
        [GetProcAddress("glDepthFunc")]
        public partial void DepthFunc(int32 value);
        
        [GetProcAddress("glDepthMask")]
        public partial void DepthMask(int32 value);

        [GetProcAddress("glClear")]
        public partial void Clear(int32 bits);

        [GetProcAddress("glViewport")]
        public partial void Viewport(int32 x, int32 y, int32 width, int32 height);

        [GetProcAddress("glFlush")]
        public partial void Flush();

        [GetProcAddress("glFinish")]
        public partial void Finish();

        [GetProcAddress("glGenFramebuffers")]
        public partial void GenFramebuffers(int32 count, int32* res);

        public int32 GenFramebuffer()
        {
            int32 rv = 0;
            GenFramebuffers(1, &rv);
            return rv;
        }

        [GetProcAddress("glDeleteFramebuffers")]
        public partial void DeleteFramebuffers(int32 count, int32* framebuffers);

        public void DeleteFramebuffer(int32 fb)
        {
            DeleteFramebuffers(1, &fb);
        }

        [GetProcAddress("glBindFramebuffer")]
        public partial void BindFramebuffer(int32 target, int32 fb);

        [GetProcAddress("glCheckFramebufferStatus")]
        public partial int32 CheckFramebufferStatus(int32 target);

        [GlMinVersionEntryPoint("glBlitFramebuffer", 3, 0), GetProcAddress(true)]
        public partial void BlitFramebuffer(int32 srcX0,
            int32 srcY0,
            int32 srcX1,
            int32 srcY1,
            int32 dstX0,
            int32 dstY0,
            int32 dstX1,
            int32 dstY1,
            int32 mask,
            int32 filter);


        [GetProcAddress("glGenRenderbuffers")]
        public partial void GenRenderbuffers(int32 count, int32* res);

        public int32 GenRenderbuffer()
        {
            int32 rv = 0;
            GenRenderbuffers(1, &rv);
            return rv;
        }

        [GetProcAddress("glDeleteRenderbuffers")]
        public partial void DeleteRenderbuffers(int32 count, int32* renderbuffers);

        public void DeleteRenderbuffer(int32 renderbuffer)
        {
            DeleteRenderbuffers(1, &renderbuffer);
        }

        [GetProcAddress("glBindRenderbuffer")]
        public partial void BindRenderbuffer(int32 target, int32 fb);

        [GetProcAddress("glRenderbufferStorage")]
        public partial void RenderbufferStorage(int32 target, int32 internalFormat, int32 width, int32 height);

        [GetProcAddress("glFramebufferRenderbuffer")]
        public partial void FramebufferRenderbuffer(int32 target, int32 attachment,
            int32 renderbufferTarget, int32 renderbuffer);

        [GetProcAddress("glGenTextures")]
        public partial void GenTextures(int32 count, int32* res);

        public int32 GenTexture()
        {
            int32 rv = 0;
            GenTextures(1, &rv);
            return rv;
        }

        [GetProcAddress("glBindTexture")]
        public partial void BindTexture(int32 target, int32 fb);

        [GetProcAddress("glActiveTexture")]
        public partial void ActiveTexture(int32 texture);

        [GetProcAddress("glDeleteTextures")]
        public partial void DeleteTextures(int32 count, int32* textures);

        public void DeleteTexture(int32 texture) => DeleteTextures(1, &texture);

        [GetProcAddress("glTexImage2D")]
        public partial void TexImage2D(int32 target, int32 level, int32 internalFormat, int32 width, int32 height, int32 border,
            int32 format, int32 type, IntPtr data);

        [GetProcAddress("glCopyTexSubImage2D")]
        public partial void CopyTexSubImage2D(int32 target, int32 level, int32 xoffset, int32 yoffset, int32 x, int32 y,
            int32 width, int32 height);

        [GetProcAddress("glTexParameteri")]
        public partial void TexParameteri(int32 target, int32 name, int32 value);


        [GetProcAddress("glFramebufferTexture2D")]
        public partial void FramebufferTexture2D(int32 target, int32 attachment,
            int32 texTarget, int32 texture, int32 level);

        [GetProcAddress("glCreateShader")]
        public partial int32 CreateShader(int32 shaderType);

        [GetProcAddress("glShaderSource")]
        public partial void ShaderSource(int32 shader, int32 count, IntPtr strings, IntPtr lengths);

        public void ShaderSourceString(int32 shader, string source)
        {
            using (var b = new Utf8Buffer(source))
            {
                var ptr = b.DangerousGetHandle();
                var len = new IntPtr(b.ByteLen);
                ShaderSource(shader, 1, new IntPtr(&ptr), new IntPtr(&len));
            }
        }

        [GetProcAddress("glCompileShader")]
        public partial void CompileShader(int32 shader);

        [GetProcAddress("glGetShaderiv")]
        public partial void GetShaderiv(int32 shader, int32 name, int32* parameters);

        [GetProcAddress("glGetShaderInfoLog")]
        public partial void GetShaderInfoLog(int32 shader, int32 maxLength, out int32 length, void* infoLog);

        public unsafe string? CompileShaderAndGetError(int32 shader, string source)
        {
            ShaderSourceString(shader, source);
            CompileShader(shader);
            int32 compiled;
            GetShaderiv(shader, GL_COMPILE_STATUS, &compiled);
            if (compiled != 0)
                return null;
            int32 logLength;
            GetShaderiv(shader, GL_INFO_LOG_LENGTH, &logLength);
            if (logLength == 0)
                logLength = 4096;
            var logData = new byte[logLength];
            int32 len;
            fixed (void* ptr = logData)
                GetShaderInfoLog(shader, logLength, out len, ptr);
            return Encoding.UTF8.GetString(logData, 0, len);
        }


        [GetProcAddress("glCreateProgram")]
        public partial int32 CreateProgram();

        [GetProcAddress("glAttachShader")]
        public partial void AttachShader(int32 program, int32 shader);

        [GetProcAddress("glLinkProgram")]
        public partial void LinkProgram(int32 program);

        [GetProcAddress("glGetProgramiv")]
        public partial void GetProgramiv(int32 program, int32 name, int32* parameters);

        [GetProcAddress("glGetProgramInfoLog")]
        public partial void GetProgramInfoLog(int32 program, int32 maxLength, out int32 len, void* infoLog);

        public unsafe string? LinkProgramAndGetError(int32 program)
        {
            LinkProgram(program);
            int32 compiled;
            GetProgramiv(program, GL_LINK_STATUS, &compiled);
            if (compiled != 0)
                return null;
            int32 logLength;
            GetProgramiv(program, GL_INFO_LOG_LENGTH, &logLength);
            var logData = new byte[logLength];
            int32 len;
            fixed (void* ptr = logData)
                GetProgramInfoLog(program, logLength, out len, ptr);
            return Encoding.UTF8.GetString(logData, 0, len);
        }

        [GetProcAddress("glBindAttribLocation")]
        public partial void BindAttribLocation(int32 program, int32 index, IntPtr name);

        public void BindAttribLocationString(int32 program, int32 index, string name)
        {
            using (var b = new Utf8Buffer(name))
                BindAttribLocation(program, index, b.DangerousGetHandle());
        }

        [GetProcAddress("glGenBuffers")]
        public partial void GenBuffers(int32 len, int32* rv);

        public int32 GenBuffer()
        {
            int32 rv;
            GenBuffers(1, &rv);
            return rv;
        }

        [GetProcAddress("glBindBuffer")]
        public partial void BindBuffer(int32 target, int32 buffer);

        [GetProcAddress("glBufferData")]
        public partial void BufferData(int32 target, IntPtr size, IntPtr data, int32 usage);

        [GetProcAddress("glGetAttribLocation")]
        public partial int32 GetAttribLocation(int32 program, IntPtr name);

        public int32 GetAttribLocationString(int32 program, string name)
        {
            using (var b = new Utf8Buffer(name))
                return GetAttribLocation(program, b.DangerousGetHandle());
        }

        [GetProcAddress("glVertexAttribPointer")]
        public partial void VertexAttribPointer(int32 index, int32 size, int32 type,
            int32 normalized, int32 stride, IntPtr pointer);

        [GetProcAddress("glEnableVertexAttribArray")]
        public partial void EnableVertexAttribArray(int32 index);

        [GetProcAddress("glUseProgram")]
        public partial void UseProgram(int32 program);

        [GetProcAddress("glDrawArrays")]
        public partial void DrawArrays(int32 mode, int32 first, IntPtr count);

        [GetProcAddress("glDrawElements")]
        public partial void DrawElements(int32 mode, int32 count, int32 type, IntPtr indices);

        [GetProcAddress("glGetUniformLocation")]
        public partial int32 GetUniformLocation(int32 program, IntPtr name);

        public int32 GetUniformLocationString(int32 program, string name)
        {
            using (var b = new Utf8Buffer(name))
                return GetUniformLocation(program, b.DangerousGetHandle());
        }

        [GetProcAddress("glUniform1f")]
        public partial void Uniform1f(int32 location, float falue);


        [GetProcAddress("glUniformMatrix4fv")]
        public partial void UniformMatrix4fv(int32 location, int32 count, bool transpose, void* value);

        [GetProcAddress("glEnable")]
        public partial void Enable(int32 what);
        
        [GetProcAddress("glDisable")]
        public partial void Disable(int32 what);

        [GetProcAddress("glDeleteBuffers")]
        public partial void DeleteBuffers(int32 count, int32* buffers);

        public void DeleteBuffer(int32 buffer) => DeleteBuffers(1, &buffer);

        [GetProcAddress("glDeleteProgram")]
        public partial void DeleteProgram(int32 program);

        [GetProcAddress("glDeleteShader")]
        public partial void DeleteShader(int32 shader);

        [GetProcAddress("glGetRenderbufferParameteriv")]
        public partial void GetRenderbufferParameteriv(int32 target, int32 name, out int32 value);
        // ReSharper restore UnassignedGetOnlyAutoProperty

        [GetProcAddress(true)]
        [GlMinVersionEntryPoint("glDeleteVertexArrays", 3, 0)]
        [GlExtensionEntryPoint("glDeleteVertexArraysOES", "GL_OES_vertex_array_object")]
        public partial void DeleteVertexArrays(int32 count, int32* arrays);

        public void DeleteVertexArray(int32 array) => DeleteVertexArrays(1, &array);

        [GetProcAddress(true)]
        [GlMinVersionEntryPoint("glBindVertexArray", 3, 0)]
        [GlExtensionEntryPoint("glBindVertexArrayOES", "GL_OES_vertex_array_object")]
        public partial void BindVertexArray(int32 array);


        [GetProcAddress(true)]
        [GlMinVersionEntryPoint("glGenVertexArrays", 3, 0)]
        [GlExtensionEntryPoint("glGenVertexArraysOES", "GL_OES_vertex_array_object")]
        public partial void GenVertexArrays(int32 n, int32* rv);

        public int32 GenVertexArray()
        {
            int32 rv = 0;
            GenVertexArrays(1, &rv);
            return rv;
        }

        public static GlInterface FromNativeUtf8GetProcAddress(GlVersion version, Func<IntPtr, IntPtr> getProcAddress)
        {
            return new GlInterface(version, s =>
            {
                var ptr = Marshal.StringToHGlobalAnsi(s);
                var rv = getProcAddress(ptr);
                Marshal.FreeHGlobal(ptr);
                return rv;
            });
        }
    }
}
