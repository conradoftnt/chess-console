using board;
namespace chess
{
    class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[board.lines, board.columns];

            Position positionToCheck = new Position(0, 0);

            // N
            positionToCheck.ChangePosition(position.line - 1, position.column);
            while (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.line, positionToCheck.column] = true;
                if (board.GetPiece(positionToCheck) != null && board.GetPiece(positionToCheck).color != color)
                {
                    break;
                }

                positionToCheck.line -= 1;
            }

            // E
            positionToCheck.ChangePosition(position.line, position.column + 1);
            while (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.line, positionToCheck.column] = true;
                if (board.GetPiece(positionToCheck) != null && board.GetPiece(positionToCheck).color != color)
                {
                    break;
                }

                positionToCheck.column += 1;
            }

            // S
            positionToCheck.ChangePosition(position.line + 1, position.column);
            while (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.line, positionToCheck.column] = true;
                if (board.GetPiece(positionToCheck) != null && board.GetPiece(positionToCheck).color != color)
                {
                    break;
                }

                positionToCheck.line += 1;
            }

            // W
            positionToCheck.ChangePosition(position.line, position.column - 1);
            while (board.ValidPosition(positionToCheck) && CanMove(positionToCheck))
            {
                possibilities[positionToCheck.line, positionToCheck.column] = true;
                if (board.GetPiece(positionToCheck) != null && board.GetPiece(positionToCheck).color != color)
                {
                    break;
                }

                positionToCheck.column -= 1;
            }

            return possibilities;
        }
    }
}