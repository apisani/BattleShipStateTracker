using System;
using BattleShipGame;

namespace BattleshipStateTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            string messageUpdate;
            bool quitNow = false;
            BattleShip game = new BattleShip(10); // Board size is customisable, however, based on the requirement, we are using a 10x10 board
            CommandExecution cmd = new CommandExecution();

            DisplayInitializeGame();
            while (!quitNow)
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "/help":
                        DisplayCommandList();
                        break;

                    case "/quit":
                        Console.WriteLine("Thank you for playing. Now exiting...");
                        quitNow = true;
                        break;

                    default:
                        messageUpdate = cmd.executeBattleShipAction(game, command.Split(' '));
                        Console.WriteLine(messageUpdate);
                        break;
                }
            }
        }

        public static void DisplayCommandList()
        {
            Console.WriteLine("Commands list:\n");
            Console.WriteLine("'addship [x] [y] [orientation] [length]'");
            Console.WriteLine("with x: number (no decimal), y: number (no decimal)");
            Console.WriteLine("orientation: 'vertical' or 'hozirontal'(without ''), length: number (no decimal)\n");
            Console.WriteLine("'attack [x] [y]'");
            Console.WriteLine("with x: number (no decimal), y: number (no decimal)\n");
            Console.WriteLine("'status' for current game status\n");
            Console.WriteLine("'\\help' for commands list\n");
            Console.WriteLine("'\\quit' to extit the game\n");
            Console.WriteLine("Please consult Read.me for more information\n");
        }

        public static void DisplayInitializeGame()
        {
            Console.WriteLine("An empty battleship board of 10x10 has been created.");
            DisplayCommandList();
        }


    }
}
