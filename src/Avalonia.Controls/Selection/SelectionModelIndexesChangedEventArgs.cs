using System;

namespace Avalonia.Controls.Selection
{
    public class SelectionModelIndexesChangedEventArgs : EventArgs
    {
        public SelectionModelIndexesChangedEventArgs(int32 startIndex, int32 delta)
        {
            StartIndex = startIndex;
            Delta = delta;
        }

        public int32 StartIndex { get; }
        public int32 Delta { get; }
    }
}
