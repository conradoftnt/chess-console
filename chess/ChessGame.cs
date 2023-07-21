using board;
using System.Collections.Generic;

namespace chess
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces { get; set; }
        private HashSet<Piece> CapturedPieces { get; set; }
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            ArrangePieces();
        }

        public Piece MakeAMove(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMove();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(piece, destiny);

            if (capturedPiece != null)
                CapturedPieces.Add(capturedPiece);

            // Castling #specialmove
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new(origin.Line, origin.Column + 3);
                Position rookDestiny = new(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMove();
                Board.PutPiece(rook, rookDestiny);
            }
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new(origin.Line, origin.Column - 4);
                Position rookDestiny = new(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementMove();
                Board.PutPiece(rook, rookDestiny);
            }

            // En Passant #specialmove
            if ((piece is Pawn) && (origin.Column != destiny.Column && capturedPiece == null))
            {
                Position pawn;
                if (piece.Color == Color.White)
                    pawn = new(destiny.Line + 1, destiny.Column);
                else
                    pawn = new(destiny.Line - 1, destiny.Column);
                capturedPiece = Board.RemovePiece(pawn);
                CapturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.DecrementMove();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                CapturedPieces.Remove(capturedPiece);
            }

            Board.PutPiece(piece, origin);

            // Castling #specialmove
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new(origin.Line, origin.Column + 3);
                Position rookDestiny = new(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementMove();
                Board.PutPiece(rook, rookOrigin);
            }
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new(origin.Line, origin.Column - 4);
                Position rookDestiny = new(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementMove();
                Board.PutPiece(rook, rookOrigin);
            }

            // En Passant #specialmove
            if ((piece is Pawn) && (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant))
            {
                Piece pawn = Board.RemovePiece(destiny);
                Position pawnPosition;
                if (piece.Color == Color.White)
                    pawnPosition = new(3, destiny.Column);
                else
                    pawnPosition = new(4, destiny.Column);
                Board.PutPiece(pawn, pawnPosition);
            }
        }

        public void TakeATurn(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeAMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece piece = Board.GetPiece(destiny);

            // Promotion #specialmove
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && destiny.Line == 0) || (piece.Color == Color.Black && destiny.Line == 7)) 
                {
                    piece = Board.RemovePiece(destiny);
                    Pieces.Remove(piece);

                    Piece queen = new Queen(piece.Color, Board);
                    Board.PutPiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
                Check = true;
            else
                Check = false;

            if (IsInCheckmate(Opponent(CurrentPlayer)))
                Finished = true;
            else
            {
                Turn++;
                ChangePlayer();
            }

            // En Passant #specialmove
            if (piece is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
                VulnerableEnPassant = piece;
            else
                VulnerableEnPassant = null;
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.GetPiece(origin) == null)
                throw new BoardException("There is no piece in the chosen position!");

            if (Board.GetPiece(origin).Color != CurrentPlayer)
                throw new BoardException("The chosen piece is not from the current player!");

            if (!Board.GetPiece(origin).IsUnblocked())
                throw new BoardException("There are no possible moves for the chosen piece!");
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).IsAPossiblePosition(destiny))
                throw new BoardException("There's not a possible destiny!");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> CapturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new();

            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new();

            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
        }

        private static Color Opponent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
                if (piece is King)
                    return piece;

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = GetKing(color) ?? throw new BoardException($"Has not a {color} king in game!");
            foreach (Piece piece in PiecesInGame(Opponent(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
                if (possibleMoves[king.Position.Line, king.Position.Column])
                    return true;
            }
            return false;
        }

        public bool IsInCheckmate(Color color)
        {
            if (!IsInCheck(color)) return false;

            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] possibleMoves = piece.PossibleMoves();

                for (int l = 0; l < Board.Lines; l++)
                {
                    for (int c = 0; c < Board.Columns; c++)
                    {
                        if (possibleMoves[l, c])
                        {
                            Position origin = piece.Position;
                            Position destiny = new(l, c);
                            Piece capturedPiece = MakeAMove(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);

                            if (!testCheck) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void ArrangeNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void ArrangePieces()
        {
            ArrangeNewPiece('a', 1, new Rook(Color.White, Board));
            ArrangeNewPiece('b', 1, new Knight(Color.White, Board));
            ArrangeNewPiece('c', 1, new Bishop(Color.White, Board));
            ArrangeNewPiece('d', 1, new Queen(Color.White, Board));
            ArrangeNewPiece('e', 1, new King(Color.White, Board, this));
            ArrangeNewPiece('f', 1, new Bishop(Color.White, Board));
            ArrangeNewPiece('g', 1, new Knight(Color.White, Board));
            ArrangeNewPiece('h', 1, new Rook(Color.White, Board));
            ArrangeNewPiece('a', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('b', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('c', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('d', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('e', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('f', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('g', 2, new Pawn(Color.White, Board, this));
            ArrangeNewPiece('h', 2, new Pawn(Color.White, Board, this));

            ArrangeNewPiece('a', 8, new Rook(Color.Black, Board));
            ArrangeNewPiece('b', 8, new Knight(Color.Black, Board));
            ArrangeNewPiece('c', 8, new Bishop(Color.Black, Board));
            ArrangeNewPiece('d', 8, new Queen(Color.Black, Board));
            ArrangeNewPiece('e', 8, new King(Color.Black, Board, this));
            ArrangeNewPiece('f', 8, new Bishop(Color.Black, Board));
            ArrangeNewPiece('g', 8, new Knight(Color.Black, Board));
            ArrangeNewPiece('h', 8, new Rook(Color.Black, Board));
            ArrangeNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            ArrangeNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    }
}
