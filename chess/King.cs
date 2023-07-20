using board;
namespace chess
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[Board.Lines, Board.Columns];

            Position positionToCheck = new (0, 0);

            // N
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // NE
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // E
            positionToCheck.ChangePosition(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SE
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // S
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SW
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // W
            positionToCheck.ChangePosition(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // NW
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            return possibilities;
        }
    }
}
