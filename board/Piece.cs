namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int AmountMoves { get; set; }
        public Board Board { get; set; }

        public Piece(Color color, Board board)
        {
            this.Position = null;
            this.Color = color;
            this.Board = board;
            this.AmountMoves = 0;
        }

        public void IncrementMove()
        {
            AmountMoves++;
        }

        public void DecrementMove() { AmountMoves--;}

        public bool IsUnblocked()
        {
            bool[,] possibleMoves = PossibleMoves();

            for (int l = 0; l < Board.Lines; l++)
                for (int c = 0; c < Board.Columns; c++)
                    if (possibleMoves[l,c])
                        return true;

            return false;
        }

        protected bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null || piece.Color != Color;
        }

        public bool IsAPossiblePosition(Position position)
        {
            return PossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
