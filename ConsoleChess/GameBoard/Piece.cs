﻿namespace GameBoard
{
    /// <summary>
    /// Movable object used on the board.
    /// </summary>
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece() { }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            MovementQuantity = 0;
            Board = board;
        }

        public void IncrementMovementQuantity()
        {
            MovementQuantity++;
        }

        public void DecrementMovementQuantity()
        {
            MovementQuantity--;
        }

        public bool PieceCanMove()
        {
            bool[,] possiblemovementPossibilitiesMatrix = PossibleMovements();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possiblemovementPossibilitiesMatrix[i, j])
                        return true;
                }
            }
            return false;
        }

        public abstract bool[,] PossibleMovements();
    }
}
