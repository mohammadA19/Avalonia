using Avalonia.Interactivity;

namespace Avalonia.Input
{
    public class ScrollGestureEventArgs : RoutedEventArgs
    {
        public int32 Id { get; }
        public Vector Delta { get; }
        /// <summary>
        /// When set the ScrollGestureRecognizer should stop its current active scroll gesture.
        /// </summary>
        public bool ShouldEndScrollGesture { get; set; }
        private static int32 _nextId = 1;

        public static int32 GetNextFreeId() => _nextId++;

        public ScrollGestureEventArgs(int32 id, Vector delta) : base(Gestures.ScrollGestureEvent)
        {
            Id = id;
            Delta = delta;
        }
    }

    public class ScrollGestureEndedEventArgs : RoutedEventArgs
    {
        public int32 Id { get; }

        public ScrollGestureEndedEventArgs(int32 id) : base(Gestures.ScrollGestureEndedEvent)
        {
            Id = id;
        }
    }

    public sealed class ScrollGestureInertiaStartingEventArgs : RoutedEventArgs
    {
        public int32 Id { get; }
        public Vector Inertia { get; }

        internal ScrollGestureInertiaStartingEventArgs(int32 id, Vector inertia) : base(Gestures.ScrollGestureInertiaStartingEvent)
        {
            Id = id;
            Inertia = inertia;
        }
    }
}
