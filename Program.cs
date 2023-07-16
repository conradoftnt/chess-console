using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ChessGame game = new ChessGame();

                while (!game.finished)
                {
                    Console.Clear();

                    Screen.ShowBoard(game.board);

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    game.MakeAMove(origin, destiny);
                }

                Screen.ShowBoard(game.board);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}