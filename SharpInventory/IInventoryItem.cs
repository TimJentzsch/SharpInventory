using System;

namespace SharpInventory
{
    /// <summary>
    /// Represents an item in an inventory.
    /// </summary>
    public interface IInventoryItem : IComparable<IInventoryItem>
    {
        /// <summary>
        /// Gets the maximum size this item can stack.
        /// </summary>
        /// <returns>The maximum item stack size.</returns>
        uint GetMaxStackSize();
        /// <summary>
        /// Gets the <see cref="string"/> representation of the item.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the item.</returns>
        string ToString();
    }
}
