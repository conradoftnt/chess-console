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

                    try
                    {
                        Console.Clear();

                        Screen.ShowGame(game);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = game.board.GetPiece(origin).PossibleMoves();

                        Console.Clear();

                        Screen.ShowBoard(game.board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinyPosition(origin, destiny);

                        game.TakeATurn(origin, destiny);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
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