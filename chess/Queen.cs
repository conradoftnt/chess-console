using board;
namespace chess
{
    class Queen : Piece
    {
        public Queen(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[Board.Lines, Board.Columns];

            Position positionToCheck = new(0, 0);

            // N
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.Line -= 1;
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

            // E
            positionToCheck.ChangePosition(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.Column += 1;
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

            // S
            positionToCheck.ChangePosition(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.Line += 1;
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

            // W
            positionToCheck.ChangePosition(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.Column -= 1;
            }

            // NW
            positionToCheck.ChangePosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.Line, positionToCheck.Column] = true;
                if (Board.GetPiece(positionToCheck) != null && Board.GetPiece(positionToCheck).Color != Color)
                    break;

                positionToCheck.ChangePosition(positionToCheck.Line - 1, positionToCheck.Column - 1);
            }

            return possibilities;
        }
    }
}