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
    }
}
