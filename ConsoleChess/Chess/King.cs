﻿using GameBoard;

namespace Chess
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMoveToPosition(Position positionOfDestination)
        {
            Piece potentialPieceAtDestination = Board.Piece(positionOfDestination);
            return potentialPieceAtDestination == null || potentialPieceAtDestination.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new Position(0, 0);

            // North of the piece
            position.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northeast of the piece
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // East of the piece
            position.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Southeast of the piece
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // South of the piece
            position.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }            
            // Southwest of the piece
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // West of the piece
            position.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northwest of the piece
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
