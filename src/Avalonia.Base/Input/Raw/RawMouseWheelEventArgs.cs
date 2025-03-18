
using Avalonia.Metadata;

namespace Avalonia.Input.Raw
{
    [PrivateApi]
    public class RawMouseWheelEventArgs : RawPointerEventArgs
    {
        public RawMouseWheelEventArgs(
            IInputDevice device,
            uint64 timestamp,
            IInputRoot root,
            Point position,
            Vector delta, RawInputModifiers inputModifiers)
            : base(device, timestamp, root, RawPointerEventType.Wheel, position, inputModifiers)
        {
            Delta = delta;
        }

        public Vector Delta { get; private set; }
    }
}
