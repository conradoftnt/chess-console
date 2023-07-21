using board;

namespace chess
{
    class ChessPosition
    {
        public int Line { get; set; }
        public char Column { get; set; }

        public ChessPosition(char column, int line)
        {
            this.Line = line;
            this.Column = column;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Line}";
        }
    }
}
