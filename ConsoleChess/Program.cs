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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(chessMatch);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(chessMatch.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write(Screen.PrintHowToPlay());

                        Console.WriteLine();
                        Console.WriteLine("Turn #" + chessMatch.Turn);
                        Console.WriteLine("Awaiting Player: " + chessMatch.CurrentPlayer);

                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ExecutePlay(origin, destination);
                    }
                    catch (BoardException exception)
                    {
                        Console.WriteLine();
                        Console.WriteLine(exception.Message);
                        Console.WriteLine("Press enter to try again.");
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}
