using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SharpInventory
{
    /// <summary>
    /// Represents an inventory.
    /// </summary>
    public class Inventory : IEnumerable<InventorySlot>
    {
        /// <summary>
        /// The array containing the inventory slots.
        /// </summary>
        protected InventorySlot[] InventorySlots { get; set; }
        /// <summary>
        /// The size of the inventory.
        /// </summary>
        public int Size => InventorySlots.Length;


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
                InventorySlot curSlot = InventorySlots[slotIndex];
                // Ignore filtered slots
                if (!curSlot.HasFilter)
                {
                    int lastSwapIndex = slotIndex;
                    // Move all bigger slots to the right
                    for (int curSwapIndex = slotIndex - 1; (curSwapIndex >= 0) && (curSlot > InventorySlots[curSwapIndex]); curSwapIndex--)
                    {
                        InventorySlot curSwap = InventorySlots[curSwapIndex];
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
        /// <param name="firstIndex">The index of the first <see cref="InventorySlot"/> to swap.</param>
        /// <param name="secondIndex">The index of the second <see cref="InventorySlot"/> to swap.</param>
        public void Swap(int firstIndex, int secondIndex)
        {
            if (firstIndex != secondIndex)
            {
                InventorySlot temp = InventorySlots[firstIndex];
                InventorySlots[firstIndex] = InventorySlots[secondIndex];
                InventorySlots[secondIndex] = temp;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> over all inventory slots.
        /// </summary>
        /// <returns>The enumerator over all inventory slots.</returns>
        public IEnumerator<InventorySlot> GetEnumerator()
        {
            return (IEnumerator<InventorySlot>) InventorySlots.GetEnumerator();
        }

        /// <summary>
        /// Gets an <see cref="IEnumerator"/> over all inventory slots.
        /// </summary>
        /// <returns>The enumerator over all inventory slots.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return InventorySlots.GetEnumerator();
        }
        #endregion
    }
}
