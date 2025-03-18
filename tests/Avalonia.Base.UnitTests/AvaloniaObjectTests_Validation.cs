using System;
using System.Reactive.Subjects;
using Avalonia.Controls;
using Avalonia.Data;
using Xunit;

namespace Avalonia.Base.UnitTests
{
    public class AvaloniaObjectTests_Validation
    {
        [Fact]
        public void Registration_Throws_If_DefaultValue_Fails_Validation()
        {
            Assert.Throws<ArgumentException>(() =>
                new StyledProperty<int32>(
                    "BadDefault",
                    typeof(Class1),
                    typeof(Class1),
                    new StyledPropertyMetadata<int32>(101),
                    validate: Class1.ValidateFoo));
        }

        [Fact]
        public void Metadata_Override_Throws_If_DefaultValue_Fails_Validation()
        {
            Assert.Throws<ArgumentException>(() => Class1.FooProperty.OverrideDefaultValue<Class2>(101));
        }

        [Fact]
        public void SetValue_Throws_If_Fails_Validation()
        {
            var target = new Class1();

            Assert.Throws<ArgumentException>(() => target.SetValue(Class1.FooProperty, 101));
        }

        [Fact]
        public void SetValue_Throws_If_Fails_Validation_Attached()
        {
            var target = new Class1();

            Assert.Throws<ArgumentException>(() => target.SetValue(Class1.AttachedProperty, 101));
        }

        [Fact]
        public void Reverts_To_DefaultValue_If_LocalValue_Binding_Fails_Validation()
        {
            var target = new Class1();
            var source = new Subject<int32>();

            target.Bind(Class1.FooProperty, source);
            source.OnNext(150);

            Assert.Equal(11, target.GetValue(Class1.FooProperty));
        }

        [Fact]
        public void Reverts_To_DefaultValue_If_Style_Binding_Fails_Validation()
        {
            var target = new Class1();
            var source = new Subject<int32>();

            target.Bind(Class1.FooProperty, source, BindingPriority.Style);
            source.OnNext(150);

            Assert.Equal(11, target.GetValue(Class1.FooProperty));
        }

        [Fact]
        public void Reverts_To_DefaultValue_If_Style_Binding_Fails_Validation_2()
        {
            var target = new Class1();
            var source = new Subject<int32>();

            target.SetValue(Class1.FooProperty, 10, BindingPriority.Style);
            target.Bind(Class1.FooProperty, source, BindingPriority.StyleTrigger);
            source.OnNext(150);

            Assert.Equal(11, target.GetValue(Class1.FooProperty));
        }

        [Theory]
        [InlineData(BindingPriority.LocalValue)]
        [InlineData(BindingPriority.Style)]
        public void Reverts_To_DefaultValue_If_Style_Binding_Fails_Validation_3(BindingPriority priority)
        {
            var target = new Class1();
            var source = new Subject<BindingValue<int32>>();

            target.Bind(Class1.FooProperty, source, priority);
            source.OnNext(150);

            Assert.Equal(11, target.GetValue(Class1.FooProperty));
        }

        [Fact]
        public void Reverts_To_DefaultValue_Even_In_Presence_Of_Other_Bindings()
        {
            var target = new Class1();
            var source1 = new Subject<int32>();
            var source2 = new Subject<int32>();

            target.Bind(Class1.FooProperty, source1);
            target.Bind(Class1.FooProperty, source2);
            source1.OnNext(42);
            source2.OnNext(150);

            Assert.Equal(11, target.GetValue(Class1.FooProperty));
        }

        private class Class1 : AvaloniaObject
        {
            public static readonly StyledProperty<int32> FooProperty =
                AvaloniaProperty.Register<Class1, int32>(
                    "Qux",
                    defaultValue: 11,
                    validate: ValidateFoo);

            public static readonly AttachedProperty<int32> AttachedProperty =
                AvaloniaProperty.RegisterAttached<Class1, Class1, int32>(
                    "Attached",
                    defaultValue: 11,
                    validate: ValidateFoo);

            public static bool ValidateFoo(int32 value)
            {
                return value < 100;
            }
        }

        private class Class2 : AvaloniaObject
        {
        }
    }
}
