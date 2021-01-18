using System;
using System.Collections.Generic;
using System.Text;
using Chess;
using GameBoard;

namespace ConsoleChess
{
    /// <summary>
    /// The Screen Class is responsible for printing the board on the Console,
    /// as well as all the pieces and their current location.
    /// </summary>
    class Screen
    {
        public static void PrintMatch(ChessMatch chessMatch)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.WriteLine("CONSOLE CHESS");
            
            Console.WriteLine();
            Screen.PrintBoard(chessMatch.Board);
            
            Console.WriteLine();
            PrintCapturedPieces(chessMatch);

            Console.WriteLine();
            Console.Write(Screen.PrintHowToPlay());

            Console.WriteLine();
            Console.WriteLine("Turn #" + chessMatch.Turn);

            if (!chessMatch.MatchIsFinished)
            {
                Console.Write("Awaiting Player: ");
                if (chessMatch.CurrentPlayer == Color.Black)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(chessMatch.CurrentPlayer);
                Console.ForegroundColor = defaultColor;
                if (chessMatch.IsInCheck)
                {
                    Console.WriteLine("Player in Check!");
                }
            }
            else
            {
                Console.WriteLine("CHECK MATE!");
                Console.WriteLine("Winner: " + chessMatch.CurrentPlayer);
                Console.WriteLine("Congratulations!");
            }
        }

        private static void PrintCapturedPieces(ChessMatch chessMatch)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.WriteLine("Captured Pieces");
            Console.Write("White: ");
            PrintListOfPieces(chessMatch.CapturedPieces(Color.White));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            PrintListOfPieces(chessMatch.CapturedPieces(Color.Black));
            Console.ForegroundColor = defaultColor;
            Console.WriteLine();
        }

        private static void PrintListOfPieces(HashSet<Piece> capturedPieces)
        {
            Console.Write("[");
            foreach (Piece piece in capturedPieces)
            {
                if (piece.Color == Color.Black) 
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            ConsoleColor alternativeBaclgroundColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                        Console.BackgroundColor = alternativeBaclgroundColor;
                    else
                        Console.BackgroundColor = originalBackgroundColor;
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackgroundColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackgroundColor;
        }

        public static ChessPosition ReadChessPosition()
        {
            string playerInput = Console.ReadLine();
            char columnInput = playerInput[0];
            int lineInput = int.Parse(playerInput[1].ToString());
            return new ChessPosition(columnInput, lineInput);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == Color.White)
                    Console.Write(piece);
                else
                {
                    ConsoleColor defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = defaultColor;
                }
                Console.Write(" ");
            }
        }

        public static string PrintHowToPlay()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("To move the pieces, enter their ");
            stringBuilder.AppendLine("current position (ex: a1) on the ");
            stringBuilder.AppendLine("Origin field and their final");
            stringBuilder.AppendLine("position on the Destination field.");

            return stringBuilder.ToString();
        }
    }
}
