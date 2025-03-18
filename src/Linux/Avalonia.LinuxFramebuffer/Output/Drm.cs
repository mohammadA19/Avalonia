using System;
using System.Runtime.InteropServices;
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Avalonia.LinuxFramebuffer.Output
{
    public enum DrmModeConnection
    {
        DRM_MODE_CONNECTED = 1,
        DRM_MODE_DISCONNECTED = 2,
        DRM_MODE_UNKNOWNCONNECTION = 3
    }
        
    public enum DrmModeSubPixel{
        DRM_MODE_SUBPIXEL_UNKNOWN        = 1,
        DRM_MODE_SUBPIXEL_HORIZONTAL_RGB = 2,
        DRM_MODE_SUBPIXEL_HORIZONTAL_BGR = 3,
        DRM_MODE_SUBPIXEL_VERTICAL_RGB   = 4,
        DRM_MODE_SUBPIXEL_VERTICAL_BGR   = 5,
        DRM_MODE_SUBPIXEL_NONE           = 6
    }
    
    static unsafe class LibDrm
    {
        private const string libdrm = "libdrm.so.2";
        private const string libgbm = "libgbm.so.1";
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void DrmEventVBlankHandlerDelegate(int32 fd,
            uint32 sequence,
            uint32 tv_sec,
            uint32 tv_usec,
            void* user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void DrmEventPageFlipHandlerDelegate(int32 fd,
            uint32 sequence,
            uint32 tv_sec,
            uint32 tv_usec,
            void* user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate IntPtr DrmEventPageFlipHandler2Delegate(int32 fd,
            uint32 sequence,
            uint32 tv_sec,
            uint32 tv_usec,
            uint32 crtc_id,
            void* user_data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void DrmEventSequenceHandlerDelegate(int32 fd,
            ulong sequence,
            ulong ns,
            ulong user_data);

        [StructLayout(LayoutKind.Sequential)]
        public struct DrmEventContext
        {
            public int32 version; //4
            public IntPtr vblank_handler;
            public IntPtr page_flip_handler;
            public IntPtr page_flip_handler2;
            public IntPtr sequence_handler;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct drmModeRes {

            public int32 count_fbs;
            public uint32 *fbs;

            public int32 count_crtcs;
            public uint32 *crtcs;

            public int32 count_connectors;
            public uint32 *connectors;

            public int32 count_encoders;
            public uint32 *encoders;

            uint32 min_width, max_width;
            uint32 min_height, max_height;
        }

        [Flags]
        public enum DrmModeType
        {
            DRM_MODE_TYPE_BUILTIN = (1 << 0),
            DRM_MODE_TYPE_CLOCK_C = ((1 << 1) | DRM_MODE_TYPE_BUILTIN),
            DRM_MODE_TYPE_CRTC_C = ((1 << 2) | DRM_MODE_TYPE_BUILTIN),
            DRM_MODE_TYPE_PREFERRED = (1 << 3),
            DRM_MODE_TYPE_DEFAULT = (1 << 4),
            DRM_MODE_TYPE_USERDEF = (1 << 5),
            DRM_MODE_TYPE_DRIVER = (1 << 6)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct drmModeModeInfo
        {
            public uint32 clock;
            public ushort hdisplay, hsync_start, hsync_end, htotal, hskew;
            public ushort vdisplay, vsync_start, vsync_end, vtotal, vscan;

            public uint32 vrefresh;

            public uint32 flags;
            public DrmModeType type;
            public fixed uint8 name[32];
            public PixelSize Resolution => new PixelSize(hdisplay, vdisplay);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct drmModeConnector {
            public uint32 connector_id;
            public uint32 encoder_id; // Encoder currently connected to 
            public uint32 connector_type;
            public uint32 connector_type_id;
            public DrmModeConnection connection;
            public uint32 mmWidth, mmHeight; //  HxW in millimeters 
            public DrmModeSubPixel subpixel;

            public int32 count_modes;
            public drmModeModeInfo* modes;

            public int32 count_props;
            public uint32 *props; // List of property ids 
            public ulong *prop_values; // List of property values 

            public int32 count_encoders;
            public uint32 *encoders; //List of encoder ids
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct drmModeEncoder {
            public uint32 encoder_id;
            public uint32 encoder_type;
            public uint32 crtc_id;
            public uint32 possible_crtcs;
            public uint32 possible_clones;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct drmModeCrtc {
            public uint32 crtc_id;
            public uint32 buffer_id; // FB id to connect to 0 = disconnect 

            public uint32 x, y; // Position on the framebuffer 
            public uint32 width, height;
            public int32 mode_valid;
            public drmModeModeInfo mode;

            public int32 gamma_size; // Number of gamma stops 

        }
        
        [DllImport(libdrm, SetLastError = true)]
        public static extern drmModeRes* drmModeGetResources(int32 fd);
        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModeFreeResources(drmModeRes* res);

        [DllImport(libdrm, SetLastError = true)]
        public static extern drmModeConnector* drmModeGetConnector(int32 fd, uint32 connector);
        [DllImport(libdrm, SetLastError = true)]
        public static extern drmModeConnector* drmModeGetConnectorCurrent(int32 fd, uint32 connector);
        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModeFreeConnector(drmModeConnector* res);
        
        [DllImport(libdrm, SetLastError = true)]
        public static extern drmModeEncoder* drmModeGetEncoder(int32 fd, uint32 id);
        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModeFreeEncoder(drmModeEncoder* enc);
        [DllImport(libdrm, SetLastError = true)]
        public static extern drmModeCrtc* drmModeGetCrtc(int32 fd, uint32 id);
        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModeFreeCrtc(drmModeCrtc* enc);

        [DllImport(libdrm, SetLastError = true)]
        public static extern int32 drmModeAddFB(int32 fd, uint32 width, uint32 height, uint8 depth,
            uint8 bpp, uint32 pitch, uint32 bo_handle,
            out uint32 buf_id);

        [DllImport(libdrm, SetLastError = true)]
        public static extern int32 drmModeAddFB2(int32 fd, uint32 width, uint32 height,
            uint32 pixel_format, uint32[] bo_handles, uint32[] pitches,
            uint32[] offsets, out uint32 buf_id, uint32 flags);

        [DllImport(libdrm, SetLastError = true)]
        public static extern int32 drmModeSetCrtc(int32 fd, uint32 crtcId, uint32 bufferId,
            uint32 x, uint32 y, uint32 *connectors, int32 count,
            drmModeModeInfo* mode);
        
        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModeRmFB(int32 fd, int32 id);

        [Flags]
        public enum DrmModePageFlip
        {
            Event = 1,
            Async = 2,
            Absolute = 4,
            Relative = 8,
        }

        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmModePageFlip(int32 fd, uint32 crtc_id, uint32 fb_id,
            DrmModePageFlip flags, void *user_data);


        [DllImport(libdrm, SetLastError = true)]
        public static extern void drmHandleEvent(int32 fd, DrmEventContext* context);

        [DllImport(libgbm, SetLastError = true)]
        public static extern IntPtr gbm_create_device(int32 fd);
        
        
        [Flags]
        public enum GbmBoFlags {
            /**
             * Buffer is going to be presented to the screen using an API such as KMS
             */
            GBM_BO_USE_SCANOUT      = (1 << 0),
            /**
             * Buffer is going to be used as cursor
             */
            GBM_BO_USE_CURSOR       = (1 << 1),
            /**
             * Deprecated
             */
            GBM_BO_USE_CURSOR_64X64 = GBM_BO_USE_CURSOR,
            /**
             * Buffer is to be used for rendering - for example it is going to be used
             * as the storage for a color buffer
             */
            GBM_BO_USE_RENDERING    = (1 << 2),
            /**
             * Buffer can be used for gbm_bo_write.  This is guaranteed to work
             * with GBM_BO_USE_CURSOR, but may not work for other combinations.
             */
            GBM_BO_USE_WRITE    = (1 << 3),
            /**
             * Buffer is linear, i.e. not tiled.
             */
            GBM_BO_USE_LINEAR = (1 << 4),
        };

        [DllImport(libgbm, SetLastError = true)]
        public static extern IntPtr gbm_surface_create(IntPtr device, int32 width, int32 height, uint32 format, GbmBoFlags flags);
        [DllImport(libgbm, SetLastError = true)]
        public static extern IntPtr gbm_surface_lock_front_buffer(IntPtr surface);
        [DllImport(libgbm, SetLastError = true)]
        public static extern int32 gbm_surface_release_buffer(IntPtr surface, IntPtr bo);
        [DllImport(libgbm, SetLastError = true)]
        public static extern IntPtr gbm_bo_get_user_data(IntPtr surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GbmBoUserDataDestroyCallbackDelegate(IntPtr bo, IntPtr data);

        [DllImport(libgbm, SetLastError = true)]
        public static extern IntPtr gbm_bo_set_user_data(IntPtr bo, IntPtr userData,
            GbmBoUserDataDestroyCallbackDelegate onFree);

        [DllImport(libgbm, SetLastError = true)]
        public static extern uint32 gbm_bo_get_width(IntPtr bo);

        [DllImport(libgbm, SetLastError = true)]
        public static extern uint32 gbm_bo_get_height(IntPtr bo);

        [DllImport(libgbm, SetLastError = true)]
        public static extern uint32 gbm_bo_get_stride(IntPtr bo);

        [DllImport(libgbm, SetLastError = true)]
        public static extern uint32 gbm_bo_get_format(IntPtr bo);

        [StructLayout(LayoutKind.Explicit)]
        public struct GbmBoHandle
        {
            [FieldOffset(0)]
            public void *ptr;
            [FieldOffset(0)]
            public int32 s32;
            [FieldOffset(0)]
            public uint32 u32;
            [FieldOffset(0)]
            public long s64;
            [FieldOffset(0)]
            public ulong u64;
        }
        
        [DllImport(libgbm, SetLastError = true)]
        public static extern GbmBoHandle gbm_bo_get_handle(IntPtr bo);

        public static  class GbmColorFormats
        {
            public static uint32 FourCC(char a, char b, char c, char d) =>
                (uint32)a | ((uint32)b) << 8 | ((uint32)c) << 16 | ((uint32)d) << 24;

            public static uint32 GBM_FORMAT_XRGB8888 { get; } = FourCC('X', 'R', '2', '4');
        }
    }
    
}
