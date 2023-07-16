namespace board
{
    class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece GetPiece(Position position) 
        {
            return pieces[position.line, position.column];
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (HasPiece(position))
            {
                throw new BoardException("Already has a piece in that position!");
            }
            pieces[position.line, position.column] = piece;
            piece.position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (!HasPiece(position))
            {
                return null;
            }
            Piece aux = GetPiece(position);
            aux.position = null;
            pieces[position.line, position.column] = null;
            return aux;

        }

        public bool ValidPosition(Position position) 
        {
            if (position.line < 0 || position.column < 0 || position.line >= lines || position.column >= columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Position invalid!");
            }
        }

        public bool HasPiece(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null; 
        }
    }
}
