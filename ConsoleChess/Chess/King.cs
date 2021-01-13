using GameBoard;

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

            // North to the Piece
            position.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northeast to the Piece
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // East to the Piece
            position.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Southeast to the Piece
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // South to the Piece
            position.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }            
            // Southwest to the Piece
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // West to the Piece
            position.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northwest to the Piece
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
