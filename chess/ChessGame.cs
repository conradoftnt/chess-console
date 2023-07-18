using board;

namespace chess
{
    class ChessGame
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            ArrangePieces();
        }

        public void MakeAMove(Position origin, Position destiny) 
        {
            Piece piece = board.RemovePiece(origin);
            piece.IncrementMove();
            Piece capturedPiece = board.RemovePiece(destiny);
            board.PutPiece(piece, destiny);

        }

        public void TakeATurn(Position origin, Position destiny)
        {
            MakeAMove(origin, destiny);
            turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (board.GetPiece(origin) == null)
            {
                throw new BoardException("There is no piece in the chosen position!");
            }

            if (board.GetPiece(origin).color != currentPlayer)
            {
                throw new BoardException("The chosen piece is not from the current player!");
            }

            if (!board.GetPiece(origin).IsUnblocked())
            {
                throw new BoardException("There are no possible moves for the chosen piece!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!board.GetPiece(origin).IsAPossiblePosition(destiny))
            {
                throw new BoardException("There's not a possible destiny!");
            }
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        private void ArrangePieces()
        {
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('c', 1).ToPosition());
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('c', 2).ToPosition());
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('d', 2).ToPosition());
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('e', 1).ToPosition());
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('e', 2).ToPosition());
            board.PutPiece(new King(Color.White, board), new ChessPosition('d', 1).ToPosition());

            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('c', 8).ToPosition());
            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('c', 7).ToPosition());
            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('d', 7).ToPosition());
            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('e', 8).ToPosition());
            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('e', 7).ToPosition());
            board.PutPiece(new King(Color.Black, board), new ChessPosition('d', 8).ToPosition());

        }
    }
}
