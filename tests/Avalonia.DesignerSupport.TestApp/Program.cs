namespace Avalonia.DesignerSupport.TestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static int32 Main(string[] args)
            => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
          => AppBuilder.Configure<App>().UsePlatformDetect();
    }
}
