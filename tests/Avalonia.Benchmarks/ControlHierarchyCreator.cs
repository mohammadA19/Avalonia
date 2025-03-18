using System.Collections.Generic;
using Avalonia.Controls;

namespace Avalonia.Benchmarks
{
    internal class ControlHierarchyCreator
    {
        public static List<Control> CreateChildren(List<Control> controls, Panel parent, int32 childCount, int32 innerCount, int32 iterations)
        {
            for (var i = 0; i < childCount; ++i)
            {
                var control = new StackPanel();
                parent.Children.Add(control);

                for (int32 j = 0; j < innerCount; ++j)
                {
                    var child = new Button() { Width = 100, Height = 50 };

                    parent.Children.Add(child);

                    controls.Add(child);
                }

                if (iterations > 0)
                {
                    CreateChildren(controls, control, childCount, innerCount, iterations - 1);
                }

                controls.Add(control);
            }

            return controls;
        }
    }
}
