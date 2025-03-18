using Avalonia;

namespace SingleProjectSandbox;

internal static class Program
{
    internal static int32 Main(string[] args) =>
        App.BuildAvaloniaApp()
            .UseAvaloniaNative()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args);
}

