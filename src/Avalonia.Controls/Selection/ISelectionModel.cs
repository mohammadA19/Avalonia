using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Avalonia.Controls.Selection
{
    public interface ISelectionModel : INotifyPropertyChanged
    {
        IEnumerable? Source { get; set; }
        bool SingleSelect { get; set; }
        int32 SelectedIndex { get; set; }
        IReadOnlyList<int32> SelectedIndexes { get; }
        object? SelectedItem { get; set; }
        IReadOnlyList<object?> SelectedItems { get; }
        int32 AnchorIndex { get; set; }
        int32 Count { get; }

        public event EventHandler<SelectionModelIndexesChangedEventArgs>? IndexesChanged;
        public event EventHandler<SelectionModelSelectionChangedEventArgs>? SelectionChanged;
        public event EventHandler? LostSelection;
        public event EventHandler? SourceReset;

        public void BeginBatchUpdate();
        public void EndBatchUpdate();
        bool IsSelected(int32 index);
        void Select(int32 index);
        void Deselect(int32 index);
        void SelectRange(int32 start, int32 end);
        void DeselectRange(int32 start, int32 end);
        void SelectAll();
        void Clear();
    }

    public static class SelectionModelExtensions
    {
        public static IDisposable BatchUpdate(this ISelectionModel model)
        {
            return new BatchUpdateOperation(model);
        }

        public record struct BatchUpdateOperation : IDisposable
        {
            private readonly ISelectionModel _owner;
            private bool _isDisposed;

            public BatchUpdateOperation(ISelectionModel owner)
            {
                _owner = owner;
                _isDisposed = false;
                owner.BeginBatchUpdate();
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    _owner?.EndBatchUpdate();
                    _isDisposed = true;
                }
            }
        }
    }
}
