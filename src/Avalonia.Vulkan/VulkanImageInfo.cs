namespace Avalonia.Vulkan;

public record struct VulkanImageInfo
{
    public uint32 Format { get; set; }
    public PixelSize PixelSize { get; set; }
    public uint64 Handle { get; set; }
    public uint32 Layout { get; set; }
    public uint32 Tiling { get; set; }
    public uint32 UsageFlags { get; set; }
    public uint32 LevelCount { get; set; }
    public uint32 SampleCount { get; set; }
    public uint64 MemoryHandle { get; set; }
    public uint64 ViewHandle { get; set; }
    public uint64 MemorySize { get; set; }
    public bool IsProtected { get; set; }
}
