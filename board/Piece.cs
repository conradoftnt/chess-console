﻿namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int amountMoves { get; set; }
        public Board board { get; set; }

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.amountMoves = 0;
        }

        public void IncrementMove()
        {
            amountMoves++;
        }

        protected bool CanMove(Position position)
        {
            Piece piece = board.GetPiece(position);

            return piece == null || piece.color != color;
        }

        public abstract bool[,] PossibleMoves();
    }
}
