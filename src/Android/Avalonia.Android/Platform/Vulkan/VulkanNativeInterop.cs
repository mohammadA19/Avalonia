using System;
using Avalonia.SourceGenerator;
using Avalonia.Vulkan;

namespace Avalonia.Android.Platform.Vulkan;
partial class AndroidVulkanInterface
{
    public AndroidVulkanInterface(IVulkanInstance instance)
    {
        Initialize(name => instance.GetInstanceProcAddress(instance.Handle, name));
    }

    [GetProcAddress("vkCreateAndroidSurfaceKHR")]
    public partial int32 vkCreateAndroidSurfaceKHR(IntPtr instance, ref VkAndroidSurfaceCreateInfoKHR pCreateInfo,
            IntPtr pAllocator, out uint64 pSurface);
}

struct VkAndroidSurfaceCreateInfoKHR
{
    public const uint32 VK_STRUCTURE_TYPE_ANDROID_SURFACE_CREATE_INFO_KHR = 1000008000;
    public uint32 sType;
    public IntPtr pNext;
    public uint32 flags;
    public IntPtr window;
}
