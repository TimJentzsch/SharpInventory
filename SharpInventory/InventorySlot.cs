using System;
using System.Collections.Generic;
using System.Text;

namespace SharpInventory
{
    /// <summary>
    /// Represents a slot in an inventory.
    /// </summary>
    public class InventorySlot<T> : IComparable<InventorySlot<T>> where T : IInventoryItem
    {
        /// <summary>
        /// The item contained in the inventory slot.
        /// </summary>
        public IInventoryItem Item { get; }
        /// <summary>
        /// The count of the item in the inventory slot.
        /// </summary>
        public uint Count { get; }
        /// <summary>
        /// The item filter for the inventory slot.
        /// </summary>
        public IInventoryItem Filter { get; }
        /// <summary>
        /// Indicates if the inventory slot is filtered.
        /// </summary>
        public bool HasFilter => Filter != null;
        /// <summary>
        /// Indicates whether the <see cref="InventorySlot{T}"/> contains any items.
        /// </summary>
        /// <returns><c>true</c>, if the <see cref="InventorySlot{T}"/> is empty, else <c>false</c>.</returns>
        public bool IsEmpty => Count == 0;

        #region Constructors
        /// <summary>
        /// Create a new <see cref="InventorySlot{T}"/> with <paramref name="count"/> of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="IInventoryItem"/> to put in the slot.</param>
        /// <param name="count">How many items to put in the slot.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="count"/> exceeds the maximum stack size of the item.</exception>
        public InventorySlot(IInventoryItem item, uint count)
        {
            if (item != null && count > item.GetMaxStackSize())
            {
                throw new ArgumentOutOfRangeException("The item count exceeds the maximum item stack size.");
            }

            Item = (count != 0) ? item : null;
            Count = (Item != null) ? count : 0;
        }

        /// <summary>
        /// Create a new <see cref="InventorySlot{T}"/> with one <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="IInventoryItem"/> to put in the slot.</param>
        public InventorySlot(IInventoryItem item)
        {
            Item = (item != null && item.GetMaxStackSize() > 0) ? item : null;
            Count = (Item != null) ? (uint)1 : 0;
        }

        /// <summary>
        /// Create a new empty <see cref="InventorySlot{T}"/>.
        /// </summary>
        public InventorySlot()
        {
            Item = null;
            Count = 0;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is smaller than the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is smaller than the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator <(InventorySlot<T> left, InventorySlot<T> right)
        {
            return left.CompareTo(right) < 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is bigger than the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is bigger than the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator >(InventorySlot<T> left, InventorySlot<T> right)
        {
            return left.CompareTo(right) > 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is smaller than or equal to the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is smaller than or equal to the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator <=(InventorySlot<T> left, InventorySlot<T> right)
        {
            return left.CompareTo(right) <= 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is bigger than or equal to the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is bigger than or equal to the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator >=(InventorySlot<T> left, InventorySlot<T> right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Compare this <see cref="InventorySlot{T}"/> to an other <see cref="InventorySlot{T}"/>.
        /// </summary>
        /// <param name="other">The other <see cref="InventorySlot{T}"/> to compare to.</param>
        /// <returns><c>-1</c>, if this instance precedes, <c>1</c>, if this instance follows and <c>0</c> 
        /// if this instance appears in the same position in the sort order as the <paramref name="other"/> parameter.</returns>
        public int CompareTo(InventorySlot<T> other)
        {
            if (other == null)
                return -1;

            if (!IsEmpty)
            {
                if (!other.IsEmpty)
                {
                    // Sort by item
                    return Item.CompareTo(other.Item);
                }
                // Empty slots last
                else return -1;
            }
            // Empty slots last
            else return 1;
        }
        /// <summary>
        /// Gets the <see cref="string"/> representation of the inventory slot.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the inventory slot.</returns>
        public override string ToString()
        {
            // Filter Item or contained Item
            string itemString = (HasFilter) ? $"{{{Filter.ToString()}}}" : ((!IsEmpty) ? $"[{Item.ToString()}]" : "[ ]");
            string countString = (!IsEmpty) ? $"({Count}/{Item.GetMaxStackSize()})" : "(0)";
            return $"{itemString} {countString}";
        }
        #endregion
    }
}
