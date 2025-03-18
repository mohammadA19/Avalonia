using System.Diagnostics;
using Avalonia.Data;

namespace Avalonia.PropertyStore
{
    internal enum FramePriority : int8
    {
        Animation,
        AnimationTemplatedParentTheme,
        AnimationTheme,
        StyleTrigger,
        StyleTriggerTemplatedParentTheme,
        StyleTriggerTheme,
        Template,
        TemplateTemplatedParentTheme,
        TemplateTheme,
        Style,
        StyleTemplatedParentTheme,
        StyleTheme,
    }

    internal static class FramePriorityExtensions
    {
        public static FramePriority ToFramePriority(this BindingPriority priority, FrameType type = FrameType.Style)
        {
            Debug.Assert(priority != BindingPriority.LocalValue);
            var p = (int32)(priority > 0 ? priority : priority + 1);
            return (FramePriority)(p * 3 + (int32)type);
        }

        public static BindingPriority ToBindingPriority(this FramePriority priority)
        {
            var p = (int32)priority / 3;
            return p == 0 ? BindingPriority.Animation : (BindingPriority)p;
        }

        public static bool IsType(this FramePriority priority, FrameType type)
        {
            return (FrameType)((int32)priority % 3) == type;
        }
    }
}
