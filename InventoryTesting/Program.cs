using System;
using SharpInventory;

namespace InventoryTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Wait()
        {
            Console.WriteLine("\n\nPress any key to continue.");
            Console.Read();
        }
    }

    class Item : IInventoryItem
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public int CompareTo(IInventoryItem other)
        {
            if (other is Item otherItem)
            {
                return Name.CompareTo(otherItem);
            }
            else
            {
                return -1;
            }
        }

        public uint GetMaxStackSize()
        {
            return 10;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
