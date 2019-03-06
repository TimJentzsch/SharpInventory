using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpInventory
{
    /// <summary>
    /// Represents an inventory.
    /// </summary>
    public class Inventory<T> : IEnumerable<InventorySlot<T>> where T : IInventoryItem
    {
        /// <summary>
        /// The array containing the inventory slots.
        /// </summary>
        protected InventorySlot<T>[] InventorySlots { get; set; }
        /// <summary>
        /// The size of the inventory.
        /// </summary>
        public int Size => InventorySlots.Length;

        /// <summary>
        /// Creates a new <see cref="Inventory{T}"/> with the given <paramref name="size"/>.
        /// </summary>
        /// <param name="size">The size of the <see cref="Inventory{T}"/>.</param>
        public Inventory(int size)
        {
            InventorySlots = new InventorySlot<T>[size];
        }

        #region Methods
        /// <summary>
        /// Sorts (and compresses) the inventory.
        /// </summary>
        /// <remarks>
        /// The method uses a modified version of Insertion Sort to ignore filtered inventory slots and to compress them.
        /// </remarks>
        public void Sort()
        {
            // Modified version of Insertion Sort that ignores filtered InventorySlots.
            // Iterate through all slots
            for (int slotIndex = 1; slotIndex < Size; slotIndex++)
            {
                InventorySlot<T> curSlot = InventorySlots[slotIndex];
                // Ignore filtered slots
                if (!curSlot.HasFilter)
                {
                    int lastSwapIndex = slotIndex;
                    // Move all bigger slots to the right
                    for (int curSwapIndex = slotIndex - 1; (curSwapIndex >= 0) && (curSlot > InventorySlots[curSwapIndex]); curSwapIndex--)
                    {
                        InventorySlot<T> curSwap = InventorySlots[curSwapIndex];
                        // Ignore filtered slots
                        if (!curSwap.HasFilter)
                        {
                            InventorySlots[lastSwapIndex] = curSwap;
                            lastSwapIndex = curSwapIndex;
                        }
                    }
                    // Insert slot at the correct position
                    InventorySlots[lastSwapIndex] = curSlot;
                }
            }
        }

        /// <summary>
        /// Swaps two inventory slots.
        /// </summary>
        /// <param name="firstIndex">The index of the first <see cref="InventorySlot{T}"/> to swap.</param>
        /// <param name="secondIndex">The index of the second <see cref="InventorySlot{T}"/> to swap.</param>
        public void Swap(int firstIndex, int secondIndex)
        {
            if (firstIndex != secondIndex)
            {
                InventorySlot<T> temp = InventorySlots[firstIndex];
                InventorySlots[firstIndex] = InventorySlots[secondIndex];
                InventorySlots[secondIndex] = temp;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> over all inventory slots.
        /// </summary>
        /// <returns>The enumerator over all inventory slots.</returns>
        public IEnumerator<InventorySlot<T>> GetEnumerator()
        {
            return (IEnumerator<InventorySlot<T>>) InventorySlots.GetEnumerator();
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> over all inventory slots.
        /// </summary>
        /// <returns>The enumerator over all inventory slots.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return InventorySlots.GetEnumerator();
        }
        /// <summary>
        /// Gets the <see cref="string"/> representation of the inventory.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the inventory.</returns>
        public override string ToString()
        {
            string[] inventoryStringQuery =
                (from slot in InventorySlots
                 select slot.ToString()).ToArray();
            return string.Join(", ", inventoryStringQuery);
        }
        #endregion
    }
}
