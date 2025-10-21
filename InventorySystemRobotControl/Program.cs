using System;
using System.Collections.Generic;
using InventorySystemRobotControl.Models;

namespace InventorySystemRobotControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Item Sorter Robot ===\n");

            var robot = new Robot();
            var inventory = new Inventory();

            // Tilføj varer til lager (navne skal matche det du leder efter)
            inventory.AddItem(new Item("A", 10));
            inventory.AddItem(new Item("B", 20));
            inventory.AddItem(new Item("C", 30));

            // Ordre 1
            var order1 = new Order
            {
                OrderLines = new List<OrderLine>
                {
                    new OrderLine(inventory.FindItem("A")!, 1),
                    new OrderLine(inventory.FindItem("B")!, 1),
                }
            };

            // Ordre 2
            var order2 = new Order
            {
                OrderLines = new List<OrderLine>
                {
                    new OrderLine(inventory.FindItem("C")!, 1),
                    new OrderLine(inventory.FindItem("A")!, 1),
                    new OrderLine(inventory.FindItem("B")!, 1),
                }
            };

            // 🔹 Behandl ordrer
            ProcessOrder(robot, order1);
            ProcessOrder(robot, order2);

            // 👋 Tilføj waving-funktionen her
            robot.WaveArm();

            Console.WriteLine("\nAlle ordrer er behandlet ✅");
        }

        static void ProcessOrder(Robot robot, Order order)
        {
            Console.WriteLine($"Behandler ordre: {order}");
            char shipmentBox = 'S';

            foreach (var line in order.OrderLines)
            {
                var itemName = line.Item.Name;
                var box = char.ToLower(itemName[0]); // A->a, B->b, C->c
                robot.MoveItem(itemName, box, shipmentBox);
            }

            Console.WriteLine("[Robot] Shipment box moved away (conveyor)\n");
        }
    }
}


