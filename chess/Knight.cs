using board;
namespace chess
{
    class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "L";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[Board.Lines, Board.Columns];

            Position positionToCheck = new(0, 0);

            // NNE
            positionToCheck.ChangePosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // NEE
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SEE
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SSE
            positionToCheck.ChangePosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SSW
            positionToCheck.ChangePosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // SWW
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // NWW
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            // NNW
            positionToCheck.ChangePosition(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;

            return possibilities;
        }
    }
}
