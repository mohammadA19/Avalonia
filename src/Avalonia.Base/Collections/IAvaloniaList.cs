using System.Collections.Generic;

namespace Avalonia.Collections
{
    /// <summary>
    /// A notifying list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    public interface IAvaloniaList<T> : IList<T>, IAvaloniaReadOnlyList<T>
    {
        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        new int32 Count { get; }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The element at the requested index.</returns>
        new T this[int32 index] { get; set; }

        /// <summary>
        /// Adds multiple items to the collection.
        /// </summary>
        /// <param name="items">The items.</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// Inserts multiple items at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="items">The items.</param>
        void InsertRange(int32 index, IEnumerable<T> items);

        /// <summary>
        /// Moves an item to a new index.
        /// </summary>
        /// <param name="oldIndex">The index of the item to move.</param>
        /// <param name="newIndex">The index to move the item to.</param>
        void Move(int32 oldIndex, int32 newIndex);

        /// <summary>
        /// Moves multiple items to a new index.
        /// </summary>
        /// <param name="oldIndex">The first index of the items to move.</param>
        /// <param name="count">The number of items to move.</param>
        /// <param name="newIndex">The index to move the items to.</param>
        void MoveRange(int32 oldIndex, int32 count, int32 newIndex);

        /// <summary>
        /// Removes multiple items from the collection.
        /// </summary>
        /// <param name="items">The items.</param>
        void RemoveAll(IEnumerable<T> items);

        /// <summary>
        /// Removes a range of elements from the collection.
        /// </summary>
        /// <param name="index">The first index to remove.</param>
        /// <param name="count">The number of items to remove.</param>
        void RemoveRange(int32 index, int32 count);
    }
}
