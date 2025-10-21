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

        // 🦾 Hovedfunktion — håndter alle ordrer
        public void ProcessOrders()
        {
            Console.WriteLine("\n=== Starter robotbehandling af ordrer ===\n");

            foreach (var order in _orders)
            {
                Console.WriteLine($"Behandler ordre med {order.OrderLines.Count} varer...");

                foreach (var line in order.OrderLines)
                {
                    string itemName = line.Item.Name;

                    // Bestem lagerposition baseret på item-navn
                    char fromBox = GetItemBox(itemName);

                    // Alle varer skal leveres til forsendelsesboks S
                    _robot.MoveItem(itemName, fromBox, 'S');
                }
            }

            Console.WriteLine("Alle ordrer er behandlet ✅");
        }

        // 🧩 Hjælpefunktion — find hvilken boks varen ligger i
        private char GetItemBox(string itemName)
        {
            return itemName.ToLower() switch
            {
                "itema" => 'a',
                "itemb" => 'b',
                "itemc" => 'c',
                _ => 'a' // fallback hvis navn ikke passer
            };
        }
    }
}