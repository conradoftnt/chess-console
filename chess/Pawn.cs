using board;
namespace chess
{
    class Pawn : Piece
    {
        private ChessGame Game { get; set; }

        public Pawn(Color color, Board board, ChessGame game) : base(color, board)
        {
            Game = game;
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

                // En Passant #specialmove
                if (Position.Line == 3)
                {
                    Position left = new(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && HaveEnemy(left) && Board.GetPiece(left) == Game.VulnerableEnPassant)
                        possibilities[left.Line - 1, left.Column] = true;
                    
                    Position right = new(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HaveEnemy(right) && Board.GetPiece(right) == Game.VulnerableEnPassant)
                        possibilities[right.Line - 1, right.Column] = true;
                }
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

                // En Passant #specialmove
                if (Position.Line == 4)
                {
                    Position left = new(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && HaveEnemy(left) && Board.GetPiece(left) == Game.VulnerableEnPassant)
                        possibilities[left.Line + 1, left.Column] = true;

                    Position right = new(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HaveEnemy(right) && Board.GetPiece(right) == Game.VulnerableEnPassant)
                        possibilities[right.Line + 1, right.Column] = true;
                }
            }

            return possibilities;
        }
    }
}
