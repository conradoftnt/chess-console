namespace board
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

        public bool IsUnblocked()
        {
            bool[,] possibleMoves = PossibleMoves();

            for (int l = 0; l < board.lines; l++)
            {
                for (int c = 0; c < board.columns; c++)
                {
                    if (possibleMoves[l,c])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool CanMove(Position position)
        {
            Piece piece = board.GetPiece(position);

            return piece == null || piece.color != color;
        }

        public bool IsAPossiblePosition(Position position)
        {
            return PossibleMoves()[position.line, position.column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
