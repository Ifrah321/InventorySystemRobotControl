using System.Collections.Generic;

namespace InventorySystemRobotControl.Models
{
    public class Inventory
    {
        public List<Item> Items { get; set; } = new();

        public void AddItem(Item item) => Items.Add(item);
        public void RemoveItem(Item item) => Items.Remove(item);
        public Item FindItem(string name) => Items.Find(i => i.Name == name);
    }
}