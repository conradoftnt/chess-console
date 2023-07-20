namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.Lines = lines;
            this.Columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece GetPiece(Position position) 
        {
            return pieces[position.Line, position.Column];
        }

        public void PutPiece(Piece piece, Position position)
        {
            if (HasPiece(position))
            {
                throw new BoardException("Already has a piece in that position!");
            }
            pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (!HasPiece(position))
            {
                return null;
            }
            Piece aux = GetPiece(position);
            aux.Position = null;
            pieces[position.Line, position.Column] = null;
            return aux;

        }

        public bool ValidPosition(Position position) 
        {
            if (position.Line < 0 || position.Column < 0 || position.Line >= Lines || position.Column >= Columns)
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
