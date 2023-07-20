using board;
namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HaveEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreePosition(Position position)
        {
            return Board.GetPiece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] possibilities = new bool[Board.Lines, Board.Columns];

            Position positionToCheck = new(0, 0);

            // White
            if (Color == Color.White)
            {
                // N
                positionToCheck.ChangePosition(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(positionToCheck) && FreePosition(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // NN
                positionToCheck.ChangePosition(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(positionToCheck) && FreePosition(positionToCheck) && AmountMoves == 0)
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // NW
                positionToCheck.ChangePosition(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(positionToCheck) && HaveEnemy(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // NE
                positionToCheck.ChangePosition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(positionToCheck) && HaveEnemy(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;
            }

            // Black
            else
            {
                // S
                positionToCheck.ChangePosition(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(positionToCheck) && FreePosition(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // SS
                positionToCheck.ChangePosition(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(positionToCheck) && FreePosition(positionToCheck) && AmountMoves == 0)
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // SW
                positionToCheck.ChangePosition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(positionToCheck) && HaveEnemy(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;

                // SE
                positionToCheck.ChangePosition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(positionToCheck) && HaveEnemy(positionToCheck))
                    possibilities[positionToCheck.Line, positionToCheck.Column] = true;
            }

            return possibilities;
        }
    }
}
