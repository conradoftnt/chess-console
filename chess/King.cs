using board;
namespace chess
{
    class King : Piece
    {

        private ChessGame Game { get; set; }

        public King(Color color, Board board, ChessGame game) : base(color, board)
        {
            this.Game = game;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool RookCanCastling(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.AmountMoves == 0;
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

            // Castling #specialmove
            if (AmountMoves == 0 && !Game.Check)
            {
                Position rookPosition = new (Position.Line, Position.Column + 3);

                if (RookCanCastling(rookPosition))
                {
                    Position ke1 = new (Position.Line, Position.Column + 1);
                    Position ke2 = new (Position.Line, Position.Column + 2);

                    if (Board.GetPiece(ke1) == null && Board.GetPiece(ke2) == null)
                        possibilities[Position.Line, Position.Column + 2] = true;
                }

                rookPosition = new(Position.Line, Position.Column - 4);

                if (RookCanCastling(rookPosition))
                {
                    Position kw1 = new(Position.Line, Position.Column - 1);
                    Position kw2 = new(Position.Line, Position.Column - 2);
                    Position kw3 = new(Position.Line, Position.Column - 3);

                    if (Board.GetPiece(kw1) == null && Board.GetPiece(kw2) == null && Board.GetPiece(kw3) == null)
                        possibilities[Position.Line, Position.Column - 2] = true;
                }
            }

            return possibilities;
        }
    }
}
