using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Avalonia.Vulkan.Interop;

namespace Avalonia.Vulkan;


static class VulkanHelpers
{
    public static uint32 MakeVersion(Version v) => MakeVersion(v.Major, v.Minor, v.Build);

    public static uint32 MakeVersion(int32 major, int32 minor, int32 patch)
    {
        return (uint32)((major << 22) | (minor << 12) | patch);
    }
    
    public const uint32 QueueFamilyIgnored = 4294967295;
}