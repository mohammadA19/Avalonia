using Avalonia;

namespace Previewer
{
    class Program
    {
        public static AppBuilder BuildAvaloniaApp()
          => AppBuilder.Configure<App>()
                .UsePlatformDetect();

        public static int32 Main(string[] args)
          => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}
