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
    }
}
