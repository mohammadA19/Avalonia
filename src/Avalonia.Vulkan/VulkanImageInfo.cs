namespace Avalonia.Vulkan;

public record struct VulkanImageInfo
{
    public uint32 Format { get; set; }
    public PixelSize PixelSize { get; set; }
    public ulong Handle { get; set; }
    public uint32 Layout { get; set; }
    public uint32 Tiling { get; set; }
    public uint32 UsageFlags { get; set; }
    public uint32 LevelCount { get; set; }
    public uint32 SampleCount { get; set; }
    public ulong MemoryHandle { get; set; }
    public ulong ViewHandle { get; set; }
    public ulong MemorySize { get; set; }
    public bool IsProtected { get; set; }
}
