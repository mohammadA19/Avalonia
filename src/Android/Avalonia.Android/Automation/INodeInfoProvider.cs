using Android.OS;
using AndroidX.Core.View.Accessibility;

namespace Avalonia.Android.Automation
{
    public interface INodeInfoProvider
    {
        int32 VirtualViewId { get; }

        bool PerformNodeAction(int32 action, Bundle? arguments);

        void PopulateNodeInfo(AccessibilityNodeInfoCompat nodeInfo);
    }
}
