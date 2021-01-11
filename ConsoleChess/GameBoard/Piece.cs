﻿namespace GameBoard
{
    class Piece
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
    }
}
