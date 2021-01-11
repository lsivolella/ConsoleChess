using GameBoard;
using Chess;
using System;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.AddPieceToBoard(new Tower(Color.Black, board), new Position(0, 0));
                board.AddPieceToBoard(new Tower(Color.Black, board), new Position(1, 3));
                board.AddPieceToBoard(new King(Color.Black, board), new Position(2, 4));

                Screen.PrintBoard(board);

                ChessPosition position = new ChessPosition('c', 7);
                Console.WriteLine(position.ToPosition());
            }
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}
