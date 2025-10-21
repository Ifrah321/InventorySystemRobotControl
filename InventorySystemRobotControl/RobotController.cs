using System;
using System.Collections.Generic;
using InventorySystemRobotControl.Models; 
namespace InventorySystemRobotControl
{
    public class RobotController
    {
        private readonly Robot _robot;
        private readonly Inventory _inventory;
        private readonly List<Order> _orders;

        public RobotController(Robot robot, Inventory inventory, List<Order> orders)
        {
            _robot = robot;
            _inventory = inventory;
            _orders = orders;
        }
        
        public void ProcessOrders()
        {
            Console.WriteLine("\n=== Starter robotbehandling af ordrer ===\n");

            foreach (var order in _orders)
            {
                Console.WriteLine($"Behandler ordre med {order.OrderLines.Count} varer...");

                foreach (var line in order.OrderLines)
                {
                    string itemName = line.Item.Name;
                    
                    char fromBox = GetItemBox(itemName);
                    
                    _robot.MoveItem(itemName, fromBox, 'S');
                }
            }

            Console.WriteLine("Alle ordrer er behandlet ");
        }
        
        private char GetItemBox(string itemName)
        {
            return itemName.ToLower() switch
            {
                "itema" => 'a',
                "itemb" => 'b',
                "itemc" => 'c',
                _ => 'a' 
            };
        }
    }
}