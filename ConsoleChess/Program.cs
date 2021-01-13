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
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.MatchIsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);
                    Console.WriteLine();
                    Console.WriteLine("To move the pieces, enter their ");
                    Console.WriteLine("current position (ex: a1) on the ");
                    Console.WriteLine("Origin field and their final");
                    Console.WriteLine("position on the Destination field.");
                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board, possiblePositions);

                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    chessMatch.ExecuteMovement(origin, destination);
                }
            }
            catch (BoardException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}
