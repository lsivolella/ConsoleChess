using GameBoard;
using System;

namespace ConsoleChest
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.PrintBoard(board);

            Console.ReadLine();
        }
    }
}
