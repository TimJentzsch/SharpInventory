using SharpInventory;

namespace InventoryUnitTests
{
    public class Item : IInventoryItem
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public int CompareTo(IInventoryItem other)
        {
            if (other != null && other is Item otherItem)
            {
                return Name.CompareTo(otherItem.Name);
            }
            else return -1;
        }

        public uint GetMaxStackSize()
        {
            return 10;
        }
    }
}
