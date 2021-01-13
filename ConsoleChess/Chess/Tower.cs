using GameBoard;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "T";
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
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Tower meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.Line = position.Line - 1;
            }
            // East to the Piece
            position.DefineValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.Column = position.Column + 1;
            }
            // South to the Piece
            position.DefineValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.Line = position.Line + 1;
            }
            // West to the Piece
            position.DefineValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.Column = position.Column - 1;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
