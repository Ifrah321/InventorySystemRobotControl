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

            // Opret robot og lager
            var robot = new Robot();
            var inventory = new Inventory();
            
            // Tilføj varer til lager
            inventory.AddItem(new Item("A", 10));
            inventory.AddItem(new Item("B", 20));
            inventory.AddItem(new Item("C", 30));
            
            // Første ordre
            var order1 = new Order
            {
                OrderLines = new List<OrderLine>
                {
                    new OrderLine(inventory.FindItem("A")!, 1),
                    new OrderLine(inventory.FindItem("B")!, 1),
                }
            };
            
            // Anden ordre
            var order2 = new Order
            {
                OrderLines = new List<OrderLine>
                {
                    new OrderLine(inventory.FindItem("C")!, 1),
                    new OrderLine(inventory.FindItem("A")!, 1),
                    new OrderLine(inventory.FindItem("B")!, 1),
                }
            };
            
            // Behandl begge ordrer
            ProcessOrder(robot, order1);
            ProcessOrder(robot, order2);
            
            // Afslut med et "hej hej" bølge-signal 😄
            robot.WaveArm();

            Console.WriteLine("\n Alle ordrer er behandlet!");
        }

        static void ProcessOrder(Robot robot, Order order)
        {
            Console.WriteLine($"\nBehandler ordre: {order}");
            char shipmentBox = 'S';

            foreach (var line in order.OrderLines)
            {
                var itemName = line.Item.Name;
                var fromBox = char.ToLower(itemName[0]); // a, b, c
                Console.WriteLine($"[Robot] Flytter {itemName} fra {fromBox} til {shipmentBox}");
                robot.MoveItem(itemName, fromBox, shipmentBox);

                // Giv robotten lidt tid til at "flytte" (ellers sker alt for hurtigt)
                System.Threading.Thread.Sleep(2000);
            }

            Console.WriteLine("[Robot] Shipment box flyttet væk (conveyor)\n");
        }
    }
}




