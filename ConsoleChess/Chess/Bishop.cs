
using GameBoard;

namespace Chess
{
    class Bishop: Piece
    {
        public Bishop(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "B";
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

            // NW of the piece
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Bishop meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.DefineValues(position.Line - 1, position.Column - 1);
            }
            // NE of the piece
            position.DefineValues(Position.Line - 1 , Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.DefineValues(position.Line - 1, position.Column + 1);
            }
            // SE of the piece
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.DefineValues(position.Line + 1, position.Column + 1);
            }
            // SW of the piece
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                    break;
                position.DefineValues(position.Line + 1, position.Column - 1);
            }

            return movementPossibilitiesMatrix;
        }
    }
}
