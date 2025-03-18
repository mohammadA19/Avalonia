using System;

namespace Avalonia.Markup.Xaml.UnitTests
{
    internal class SampleAvaloniaObject : AvaloniaObject
    {
        public static readonly StyledProperty<string> StringProperty =
            AvaloniaProperty.Register<AvaloniaObject, string>("StrProp", string.Empty);

        public static readonly StyledProperty<int32> IntProperty =
            AvaloniaProperty.Register<AvaloniaObject, int32>("IntProp");

        public int32 Int
        {
            get => GetValue(IntProperty);
            set => SetValue(IntProperty, value);
        }

        public string String
        {
            get => GetValue(StringProperty);
            set => SetValue(StringProperty, value);
        }
    }
}
