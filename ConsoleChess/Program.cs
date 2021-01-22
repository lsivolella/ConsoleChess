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
                        Screen.PrintMatchOriginPlay(chessMatch);

                        Position origin = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintMatchDestinationPlay(chessMatch, possiblePositions);

                        Position destination = Screen.ReadChessPosition().ToPosition();
                        chessMatch.ValidateDestinationPosition(origin, destination);
                        
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
                Console.Clear();
                Screen.PrintMatchOriginPlay(chessMatch);
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
