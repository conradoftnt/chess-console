using board;

namespace chess
{
    class ChessGame
    {
        public Board board { get; private set; }
        private int turn { get; set; }
        private Color currentPlayer { get; set; }
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

        private void ArrangePieces()
        {
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('c', 1).ToPosition());
            
        }
    }
}
