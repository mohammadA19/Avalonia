using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PInvoke;

public partial class MainWindow : Window
{
    [DllImport(@"libhello")]
    extern static int32 add(int32 a, int32 b);

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        var x = add(1, 2);
    }
}
