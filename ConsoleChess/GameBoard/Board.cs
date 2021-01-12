namespace GameBoard
{
    /// <summary>
    /// The Board is responsible for controlling the positioning of each piece available on the game.
    /// Only one entity of the board may exist per match.
    /// </summary>
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board() { }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        /// <summary>
        /// Returns the Piece that occupies the given position.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        /// <summary>
        /// Returns the Piece that occupies the given position.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Piece Piece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool PositionOccupied(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void AddPieceToBoard(Piece piece, Position position)
        {
            if (PositionOccupied(position))
                throw new BoardException("ERROR: Cannot place two Pieces at the same Position.");
            // Insert the piece at the defined position on the Board Class
            Pieces[position.Line, position.Column] = piece;
            // Register the piece's position on the Piece Class
            piece.Position = position;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines
                || position.Column < 0 || position.Column >= Columns)
                return false;
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
                throw new BoardException("ERROR: Invalid Position detected.");
        }
    }
}
