using System;
using Avalonia.Interactivity;

namespace Avalonia.Input
{
    public class PullGestureEventArgs : RoutedEventArgs
    {
        public int32 Id { get; }
        public Vector Delta { get; }
        public PullDirection PullDirection { get; }

        private static int32 _nextId = 1;

        internal static int32 GetNextFreeId() => _nextId++;
        
        public PullGestureEventArgs(int32 id, Vector delta, PullDirection pullDirection) : base(Gestures.PullGestureEvent)
        {
            Id = id;
            Delta = delta;
            PullDirection = pullDirection;
        }
    }

    public class PullGestureEndedEventArgs : RoutedEventArgs
    {
        public int32 Id { get; }
        public PullDirection PullDirection { get; }

        public PullGestureEndedEventArgs(int32 id, PullDirection pullDirection) : base(Gestures.PullGestureEndedEvent)
        {
            Id = id;
            PullDirection = pullDirection;
        }
    }

    public enum PullDirection
    {
        TopToBottom,
        BottomToTop,
        LeftToRight,
        RightToLeft
    }
}
