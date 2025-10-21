using System.Collections.Generic;
using System.Linq;

namespace InventorySystemRobotControl.Models
{
    public class Order
    {
        public List<OrderLine> OrderLines { get; set; } = new();

        public double TotalPrice() => OrderLines.Sum(line => line.LineTotal());

        public override string ToString()
        {
            string itemList = string.Join(", ", OrderLines.Select(l => l.Item.Name));
            return $"{itemList} - {TotalPrice()} kr";
        }
    }
}