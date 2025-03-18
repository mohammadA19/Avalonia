using Avalonia;

namespace VirtualizationDemo;

class Program
{
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();

    public static int32 Main(string[] args)
        => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
}
