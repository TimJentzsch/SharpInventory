using System;
using System.Collections.Generic;
using System.Text;

namespace SharpInventory
{
    /// <summary>
    /// Represents a slot in an inventory.
    /// </summary>
    public class InventorySlot : IComparable<InventorySlot>
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

        #region Constructors
        /// <summary>
        /// Create a new <see cref="InventorySlot"/> with <paramref name="count"/> of <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="IInventoryItem"/> to put in the slot.</param>
        /// <param name="count">How many items to put in the slot.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="count"/> exceeds the maximum stack size of the item.</exception>
        public InventorySlot(IInventoryItem item, uint count)
        {
            if (count > item.GetMaxStackSize())
            {
                throw new ArgumentOutOfRangeException("The item count exceeds the maximum item stack size.");
            }

            Item = (count != 0) ? item : null;
            Count = (Item != null) ? count : 0;
        }

        /// <summary>
        /// Create a new <see cref="InventorySlot"/> with one <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="IInventoryItem"/> to put in the slot.</param>
        public InventorySlot(IInventoryItem item)
        {
            Item = (item != null && item.GetMaxStackSize() > 0) ? item : null;
            Count = (Item != null) ? (uint)1 : 0;
        }

        /// <summary>
        /// Create a new empty <see cref="InventorySlot"/>.
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
        public static bool operator <(InventorySlot left, InventorySlot right)
        {
            return left.CompareTo(right) < 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is bigger than the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is bigger than the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator >(InventorySlot left, InventorySlot right)
        {
            return left.CompareTo(right) > 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is smaller than or equal to the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is smaller than or equal to the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator <=(InventorySlot left, InventorySlot right)
        {
            return left.CompareTo(right) <= 0;
        }
        /// <summary>
        /// Indicates whether the <paramref name="left"/> inventory slot is bigger than or equal to the <paramref name="right"/> one.
        /// </summary>
        /// <param name="left">The left item to compare.</param>
        /// <param name="right">The right item to compare.</param>
        /// <returns><c>true</c>, if the <paramref name="left"/> slot is bigger than or equal to the <paramref name="right"/> slot, else <c>false</c>.</returns>
        public static bool operator >=(InventorySlot left, InventorySlot right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Indicates whether the <see cref="InventorySlot"/> contains any items.
        /// </summary>
        /// <returns><c>true</c>, if the <see cref="InventorySlot"/> is empty, else <c>false</c>.</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Compare this <see cref="InventorySlot"/> to an other <see cref="InventorySlot"/>.
        /// </summary>
        /// <param name="other">The other <see cref="InventorySlot"/> to compare to.</param>
        /// <returns><c>-1</c>, if this instance precedes, <c>1</c>, if this instance follows and <c>0</c> 
        /// if this instance appears in the same position in the sort order as the <paramref name="other"/> parameter.</returns>
        public int CompareTo(InventorySlot other)
        {
            if (other == null)
                return -1;

            if (!IsEmpty())
            {
                if (!other.IsEmpty())
                {
                    if (Item.CompareTo(other.Item) != 0)
                    {
                        // Sort by item
                        return Item.CompareTo(other.Item);
                    }
                    else
                    {
                        // Fuller slots first
                        return -(Count.CompareTo(other.Count));
                    }
                }
                // Empty slots last
                else return -1;
            }
            // Empty slots last
            else return 1;
        }
        #endregion
    }
}
