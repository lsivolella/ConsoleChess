using GameBoard;
using System.Collections.Generic;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool MatchIsFinished { get; private set; }
        public bool IsInCheck { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        private HashSet<Piece> piecesInGame;
        private HashSet<Piece> piecesCaptured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            MatchIsFinished = false;
            IsInCheck = false;
            Turn = 1;
            CurrentPlayer = Color.White;
            piecesInGame = new HashSet<Piece>();
            piecesCaptured = new HashSet<Piece>();
            PlacePiecesOnBoard();
        }

        private Piece ExecuteMovement(Position origin, Position destination)
        {
            Piece pieceToMove = Board.RemovePieceFromBoard(origin);
            pieceToMove.IncrementMovementQuantity();
            Piece pieceToCapture = Board.RemovePieceFromBoard(destination);
            if (pieceToCapture != null)
                piecesCaptured.Add(pieceToCapture);
            Board.AddPieceToBoard(pieceToMove, destination);
            // Special Play - Castling Short
            ExecuteCastlingShort(pieceToMove, origin, destination);
            ExecuteCastlingLong(pieceToMove, origin, destination);
            return pieceToCapture;
        }

        private void ExecuteCastlingShort(Piece pieceToMove, Position origin, Position destination)
        {
            if (pieceToMove is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestination = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePieceFromBoard(rookOrigin);
                rook.IncrementMovementQuantity();
                Board.AddPieceToBoard(rook, rookDestination);
            }
        }

        private void ExecuteCastlingLong(Piece pieceToMove, Position origin, Position destination)
        {
            if (pieceToMove is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestination = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePieceFromBoard(rookOrigin);
                rook.IncrementMovementQuantity();
                Board.AddPieceToBoard(rook, rookDestination);
            }
        }

        private void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece pieceToGoBack = Board.RemovePieceFromBoard(destination);
            pieceToGoBack.DecrementMovementQuantity();
            if (capturedPiece != null)
            {
                Board.AddPieceToBoard(capturedPiece, destination);
                piecesCaptured.Remove(capturedPiece);
            }
            Board.AddPieceToBoard(capturedPiece, origin);
            UndoCastlingShort(pieceToGoBack, origin, destination);
            UndoCastlingLong(pieceToGoBack, origin, destination);
        }

        private void UndoCastlingShort(Piece pieceToGoBack, Position origin, Position destination)
        {
            if (pieceToGoBack is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestination = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePieceFromBoard(rookDestination);
                rook.DecrementMovementQuantity();
                Board.AddPieceToBoard(rook, rookOrigin);
            }
        }

        private void UndoCastlingLong(Piece pieceToGoBack, Position origin, Position destination)
        {
            if (pieceToGoBack is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestination = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePieceFromBoard(rookDestination);
                rook.DecrementMovementQuantity();
                Board.AddPieceToBoard(rook, rookOrigin);
            }
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);
            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You cannot place your own King in check. Your play will be undone.");
            }
            if (IsKingInCheck(FindAdversaryColor(CurrentPlayer)))
                IsInCheck = true;
            else
                IsInCheck = false;
            if (IsKingInCheckMate(FindAdversaryColor(CurrentPlayer)))
                MatchIsFinished = true;
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new BoardException("There are no Pieces at the selected position.");
            if (CurrentPlayer != Board.Piece(position).Color)
                throw new BoardException("A Player cannot move the adversary's Pieces.");
            if (!Board.Piece(position).CanPieceMove())
                throw new BoardException("The Piece selected cannot make any moves.");
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveToDestination(destination))
                throw new BoardException("Invalid destination. Press enter to try again.");
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
            // White Pieces, first line
            PlaceNewPiece('a', 1, new Rook(Color.White, Board));
            PlaceNewPiece('b', 1, new Knight(Color.White, Board));
            PlaceNewPiece('c', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('d', 1, new Queen(Color.White, Board));
            PlaceNewPiece('e', 1, new King(Color.White, Board, this));
            PlaceNewPiece('f', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('g', 1, new Knight(Color.White, Board));
            PlaceNewPiece('h', 1, new Rook(Color.White, Board));
            // White Pieces, second line
            PlaceNewPiece('a', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('b', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('c', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('d', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('e', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('f', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('g', 2, new Pawn(Color.White, Board));
            PlaceNewPiece('h', 2, new Pawn(Color.White, Board));
            // Black Pieces first line
            PlaceNewPiece('a', 8, new Rook(Color.Black, Board));
            PlaceNewPiece('b', 8, new Knight(Color.Black, Board));
            PlaceNewPiece('c', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('d', 8, new Queen(Color.Black, Board));
            PlaceNewPiece('e', 8, new King(Color.Black, Board, this));
            PlaceNewPiece('f', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('g', 8, new Knight(Color.Black, Board));
            PlaceNewPiece('h', 8, new Rook(Color.Black, Board));
            // Black Pieces, second line
            PlaceNewPiece('a', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('b', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('c', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('d', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('e', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('f', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('g', 7, new Pawn(Color.Black, Board));
            PlaceNewPiece('h', 7, new Pawn(Color.Black, Board));

        }

        private Color FindAdversaryColor(Color currentPlayerColor)
        {
            if (currentPlayerColor == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece FindKingOnBoard(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                    return piece;
            }
            return null;
        }

        private bool IsKingInCheck(Color color)
        {
            Piece adversaryKing = FindKingOnBoard(color);
            if (adversaryKing == null)
                throw new BoardException("Could not find a " + color + "King on the board.");

            foreach (Piece piece in PiecesInGame(FindAdversaryColor(color)))
            {
                bool[,] possibleMovements = piece.PossibleMovements();
                if (possibleMovements[adversaryKing.Position.Line, adversaryKing.Position.Column])
                    return true;
            }
            return false;
        }

        private bool IsKingInCheckMate(Color color)
        {
            if (!IsKingInCheck(color))
                return false;
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] possibleMovementsMatrix = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMovementsMatrix[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool isKingInCheck = IsKingInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!isKingInCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
