using GameBoard;
using System.Collections.Generic;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool MatchIsFinished { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        private HashSet<Piece> piecesInGame;
        private HashSet<Piece> piecesCaptured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            MatchIsFinished = false;
            Turn = 1;
            CurrentPlayer = Color.White;
            piecesInGame = new HashSet<Piece>();
            piecesCaptured = new HashSet<Piece>();
            PlacePiecesOnBoard();
        }

        private void ExecuteMovement(Position origin, Position destination)
        {
            Piece pieceToMove = Board.RemovePieceFromBoard(origin);
            pieceToMove.IncrementMovementQuantity();
            Piece pieceToCapture = Board.RemovePieceFromBoard(destination);
            if (pieceToCapture != null)
                piecesCaptured.Add(pieceToCapture);
            Board.AddPieceToBoard(pieceToMove, destination);
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            ExecuteMovement(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new BoardException("There are no Pieces at the selected position.");
            if (CurrentPlayer != Board.Piece(position).Color)
                throw new BoardException("A Player cannot move the adversary's Pieces.");
            if (!Board.Piece(position).PieceCanMove())
                throw new BoardException("The Piece selected cannot make any moves.");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> CapturedPieces(Color pieceColor)
        {
            HashSet<Piece> auxList = new HashSet<Piece>();
            foreach (Piece piece in piecesCaptured)
            {
                if (piece.Color == pieceColor)
                    auxList.Add(piece);
            }
            return auxList;
        }

        public HashSet<Piece> PiecesInGame(Color pieceColor)
        {
            HashSet<Piece> auxList = new HashSet<Piece>();
            foreach (Piece piece in piecesInGame)
            {
                if (piece.Color == pieceColor)
                    auxList.Add(piece);
            }
            auxList.ExceptWith(CapturedPieces(pieceColor));
            return auxList;
        }

        private void PlaceNewPiece(char column, int line, Piece piece)
        {
            Board.AddPieceToBoard(piece, new ChessPosition(column, line).ToPosition());
            piecesInGame.Add(piece);
        }

        private void PlacePiecesOnBoard()
        {
            PlaceNewPiece('c', 1, new Tower(Color.White, Board));
            PlaceNewPiece('c', 2, new Tower(Color.White, Board));
            PlaceNewPiece('d', 2, new Tower(Color.White, Board));
            PlaceNewPiece('e', 1, new Tower(Color.White, Board));
            PlaceNewPiece('e', 2, new Tower(Color.White, Board));
            PlaceNewPiece('d', 1, new King(Color.White, Board));

            PlaceNewPiece('c', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('c', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('e', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('e', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
