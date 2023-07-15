using board;
using xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.PutPiece(new Tower(Color.White, board), new Position(0,0));
            board.PutPiece(new Tower(Color.Black, board), new Position(1, 3));
            board.PutPiece(new King(Color.Black, board), new Position(2, 4));

            Screen.ShowBoard(board);

            Console.ReadLine();

        }
    }
}