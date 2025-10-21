namespace InventorySystemRobotControl.Models
{
    public class OrderLine
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public OrderLine(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public double LineTotal() => Item.Price * Quantity;
    }
}