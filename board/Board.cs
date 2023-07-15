namespace board
{
    class Board
    {
        public int lines { get; set; }
        public int cloumns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int cloumns)
        {
            this.lines = lines;
            this.cloumns = cloumns;
            pieces = new Piece[lines, cloumns];
        }

        public Piece Piece(int line, int column)
        {
            return pieces[line, column];
        }

        public void PutPiece(Piece piece, Position position)
        {
            pieces[position.line, position.column] = piece;
            piece.position = position;
        }
    }
}
