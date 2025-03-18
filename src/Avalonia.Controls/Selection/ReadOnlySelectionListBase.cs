using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Collections;

namespace Avalonia.Controls.Selection;

internal abstract class ReadOnlySelectionListBase<T> : IReadOnlyList<T?>, IList, INotifyCollectionChanged
{
    public abstract T? this[int32 index] { get; }
    public abstract int32 Count { get; }

    object? IList.this[int32 index] 
    { 
        get => this[index];
        set => ThrowReadOnlyException();
    }

    bool IList.IsFixedSize => false;
    bool IList.IsReadOnly => true;
    bool ICollection.IsSynchronized => false;
    object ICollection.SyncRoot => this;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public abstract IEnumerator<T?> GetEnumerator();
    public void RaiseCollectionReset() => CollectionChanged?.Invoke(this, EventArgsCache.ResetCollectionChanged);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    int32 IList.Add(object? value) { ThrowReadOnlyException(); return 0; }
    void IList.Clear() => ThrowReadOnlyException();
    void IList.Insert(int32 index, object? value) => ThrowReadOnlyException();
    void IList.Remove(object? value) => ThrowReadOnlyException();
    void IList.RemoveAt(int32 index) => ThrowReadOnlyException();
    bool IList.Contains(object? value) => Count != 0 && ((IList)this).IndexOf(value) != -1;

    void ICollection.CopyTo(Array array, int32 index)
    {
        foreach (var item in this)
            array.SetValue(item, index++);
    }

    int32 IList.IndexOf(object? value)
    {
        for (int32 i = 0; i < Count; i++)
        {
            if (Equals(this[i], value))
                return i;
        }

        return -1;
    }

    [DoesNotReturn]
    private static void ThrowReadOnlyException() => throw new NotSupportedException("Collection is read-only.");
}
