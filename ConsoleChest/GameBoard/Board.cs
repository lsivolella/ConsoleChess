namespace GameBoard
{
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

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public void AddPieceToBoard(Piece piece, Position position)
        {
            // Insert the piece at the defined position on the Board Class
            Pieces[position.Line, position.Column] = piece;
            // Register the piece's position on the Piece Class
            piece.Position = position;
        }
    }
}
