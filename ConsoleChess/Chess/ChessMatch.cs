using GameBoard;
using System;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool MatchIsFinished { get; private set; }
        private int turn;
        private Color currentPlayer;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            MatchIsFinished = false;
            turn = 1;
            currentPlayer = Color.White;
            PlacePiecesOnBoard();
        }

        public void ExecuteMovement(Position origin, Position destination)
        {
            Piece pieceToMove = Board.RemovePieceFromBoard(origin);
            pieceToMove.IncrementMovementQuantity();
            Piece pieceToCapture = Board.RemovePieceFromBoard(destination);
            Board.AddPieceToBoard(pieceToMove, destination);
        }

        private void PlacePiecesOnBoard()
        {
            Board.AddPieceToBoard(new Tower(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.AddPieceToBoard(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.AddPieceToBoard(new Tower(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.AddPieceToBoard(new Tower(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.AddPieceToBoard(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
