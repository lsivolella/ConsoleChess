using System;
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
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                        Console.Write("- ");
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
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
            if (piece.Color == Color.White)
                Console.Write(piece);
            else
            {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}
