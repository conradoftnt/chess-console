﻿using board;
using System.Collections.Generic;

namespace chess
{
    class ChessGame
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces { get; set; }
        private HashSet<Piece> capturedPieces { get; set; }
        public bool check { get; private set; }

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            ArrangePieces();
        }

        public Piece MakeAMove(Position origin, Position destiny) 
        {
            Piece piece = board.RemovePiece(origin);
            piece.IncrementMove();
            Piece capturedPiece = board.RemovePiece(destiny);
            board.PutPiece(piece, destiny);

            if (capturedPiece != null)
                capturedPieces.Add(capturedPiece);

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = board.RemovePiece(destiny);
            piece.DecrementMove();

            if (capturedPiece != null)
            {
                board.PutPiece(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            }

            board.PutPiece(piece, origin);
        }

        public void TakeATurn(Position origin, Position destiny)
        {
            Piece capturedPiece = MakeAMove(origin, destiny);

            if (ItsCheck(currentPlayer))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (ItsCheck(Opponent(currentPlayer)))
                check = true; 
            else 
                check = false;

            turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (board.GetPiece(origin) == null)
                throw new BoardException("There is no piece in the chosen position!");

            if (board.GetPiece(origin).color != currentPlayer)
                throw new BoardException("The chosen piece is not from the current player!");
            
            if (!board.GetPiece(origin).IsUnblocked())
                throw new BoardException("There are no possible moves for the chosen piece!");
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!board.GetPiece(origin).IsAPossiblePosition(destiny))
                throw new BoardException("There's not a possible destiny!");
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
                currentPlayer = Color.Black;
            else
                currentPlayer = Color.White;
        }

        public HashSet<Piece> capturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in capturedPieces)
            {
                if (piece.color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in pieces)
            {
                if (piece.color == color)
                    aux.Add(piece);
            }

            aux.ExceptWith(capturedPiecesByColor(color));
            return aux;
        }

        private Color Opponent(Color color)
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

        public bool ItsCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
                throw new BoardException($"Has not a {color} king in game!");
            
            foreach (Piece piece in PiecesInGame(Opponent(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
                if (possibleMoves[king.position.line, king.position.column])
                    return true;
            }
            return false;
        }

        public void ArrangeNewPiece(char column, int line, Piece piece)
        {
            board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        private void ArrangePieces()
        {
            ArrangeNewPiece('c', 1, new Tower(Color.White, board));
            ArrangeNewPiece('c', 2, new Tower(Color.White, board));
            ArrangeNewPiece('d', 2, new Tower(Color.White, board));
            ArrangeNewPiece('e', 1, new Tower(Color.White, board));
            ArrangeNewPiece('e', 2, new Tower(Color.White, board));
            ArrangeNewPiece('d', 1, new King(Color.White, board));

            ArrangeNewPiece('c', 8, new Tower(Color.Black, board));
            ArrangeNewPiece('c', 7, new Tower(Color.Black, board));
            ArrangeNewPiece('d', 7, new Tower(Color.Black, board));
            ArrangeNewPiece('e', 8, new Tower(Color.Black, board));
            ArrangeNewPiece('e', 7, new Tower(Color.Black, board));
            ArrangeNewPiece('d', 8, new King(Color.Black, board));

        }
    }
}
