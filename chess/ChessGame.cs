﻿using board;
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
        }

        public void TakeATurn(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeAMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
                Check = true; 
            else 
                Check = false;

            Turn++;
            ChangePlayer();
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
            HashSet<Piece> aux = new ();

            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new ();

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



        public void ArrangeNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void ArrangePieces()
        {
            ArrangeNewPiece('c', 1, new Tower(Color.White, Board));
            ArrangeNewPiece('c', 2, new Tower(Color.White, Board));
            ArrangeNewPiece('d', 2, new Tower(Color.White, Board));
            ArrangeNewPiece('e', 1, new Tower(Color.White, Board));
            ArrangeNewPiece('e', 2, new Tower(Color.White, Board));
            ArrangeNewPiece('d', 1, new King(Color.White, Board));

            ArrangeNewPiece('c', 8, new Tower(Color.Black, Board));
            ArrangeNewPiece('c', 7, new Tower(Color.Black, Board));
            ArrangeNewPiece('d', 7, new Tower(Color.Black, Board));
            ArrangeNewPiece('e', 8, new Tower(Color.Black, Board));
            ArrangeNewPiece('e', 7, new Tower(Color.Black, Board));
            ArrangeNewPiece('d', 8, new King(Color.Black, Board));

        }
    }
}
