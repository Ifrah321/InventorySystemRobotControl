using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

namespace InventorySystemRobotControl
{
    public class Robot
    {
        private readonly Dictionary<char, double[]> boxPositions = new()
        {
            { 'a', new double[] { 0.2, -0.3, 0.1, 0, -3.1415, 0 } },
            { 'b', new double[] { 0.3, -0.3, 0.1, 0, -3.1415, 0 } },
            { 'c', new double[] { 0.4, -0.3, 0.1, 0, -3.1415, 0 } },
            { 'S', new double[] { 0.5, -0.3, 0.1, 0, -3.1415, 0 } }
        };

        public Robot()
        {
      
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        public void SendProgram(string program, uint item_id)
        {
            Console.WriteLine($"[Robot] Program sent:\n{program}\nitem_id: {item_id}");
        }

        public string GenerateURScript(double[] startPose, double[] endPose)
        {
            return $@"
def move_item():
    p_start = p[{startPose[0]}, {startPose[1]}, {startPose[2]}, {startPose[3]}, {startPose[4]}, {startPose[5]}]
    p_end   = p[{endPose[0]}, {endPose[1]}, {endPose[2]}, {endPose[3]}, {endPose[4]}, {endPose[5]}]
    movej(get_inverse_kin(p_start))
    movej(get_inverse_kin(p_end))
end
";
        }
        
        public void MoveItem(string itemName, char fromBox, char toBox)
        {
            Console.WriteLine($"[Robot] Picking {itemName} from {fromBox}, placing in {toBox}");
            
            if (!boxPositions.ContainsKey(fromBox) || !boxPositions.ContainsKey(toBox))
            {
                Console.WriteLine("[Robot]  Error: Unknown box position!");
                return;
            }
            
            var startPose = boxPositions[fromBox];
            var endPose = boxPositions[toBox];
            
            string urProgram = GenerateURScript(startPose, endPose);
            
            SendProgram(urProgram, 0);

            Console.WriteLine($"[Robot] Executed URScript for {itemName}\n");
        }
        
        public void WaveArm()
        {
            Console.WriteLine("[Robot] Starting waving motion ");

            string urScript = @"
def wave():
    # Move to neutral position
    movej([0, -3.14/2, 0, -3.14/2, 0, 0])
    i = 0
    while (i < 3):
        movej([0.3, -3.14/2, 0, -3.14/2, 0, 0])
        movej([-0.3, -3.14/2, 0, -3.14/2, 0, 0])
        i = i + 1
    end
end
";

            SendProgram(urScript, 0);

            Console.WriteLine("[Robot] Completed waving motion ");
        }
    }
}
