using board;

namespace xadrez
{
    class PositionXadrez
    {
        public int line { get; set; }
        public char column { get; set; }

        public PositionXadrez(int line, char column)
        {
            this.line = line;
            this.column = column;
        }

        public Position ToPosition()
        {
            return new Position(8 - line, column - 'a');
        }

        public override string ToString()
        {
            return $"{column}{line}";
        }
    }
}
