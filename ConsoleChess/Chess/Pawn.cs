using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "P";
        }

        private bool IsEnemyAtPosition(Position positionOfDestination)
        {
            Piece piece = Board.Piece(positionOfDestination);
            return piece != null && piece.Color != Color;
        }

        private bool IsPositionFree(Position positionAtDestination)
        {
            return Board.Piece(positionAtDestination) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                // North of the piece. Standard movement of a Pawn
                position.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // North of the piece. Initial movement for a Pawn (moves two places instead of one)
                position.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position) && MovementQuantity == 0)
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // NW of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // NE of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            else
            {
                // South of the piece. Standard movement of a Pawn
                position.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // South of the piece. Initial movement for a Pawn (moves two places instead of one)
                position.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position) && MovementQuantity == 0)
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // SW of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // SE of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
