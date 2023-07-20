using board;
namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[Board.Lines, Board.Columns];

            Position positionToCheck = new (0, 0);

            // NW
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.ChangePosition(positionToCheck.Line - 1, positionToCheck.Column - 1);
            }

            // NE
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.ChangePosition(positionToCheck.Line - 1, positionToCheck.Column + 1);
            }

            // SE
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.ChangePosition(positionToCheck.Line + 1, positionToCheck.Column + 1);
            }

            // SW
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.ChangePosition(positionToCheck.Line + 1, positionToCheck.Column - 1);
            }

            return possibilities;
        }
    }
}