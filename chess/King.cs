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
            bool[,] possibilities = new bool[board.lines, board.columns];

            Position positionToCheck = new Position(0, 0);

            // N
            positionToCheck.ChangePosition(position.line - 1, position.column);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // NE
            positionToCheck.ChangePosition(position.line - 1, position.column + 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // E
            positionToCheck.ChangePosition(position.line, position.column + 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // SE
            positionToCheck.ChangePosition(position.line + 1, position.column + 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // S
            positionToCheck.ChangePosition(position.line + 1, position.column);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // SW
            positionToCheck.ChangePosition(position.line + 1, position.column - 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // W
            positionToCheck.ChangePosition(position.line, position.column - 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            // NW
            positionToCheck.ChangePosition(position.line - 1, position.column - 1);
            if (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.line, positionToCheck.column] = true;

            return possibilities;
        }
    }
}
