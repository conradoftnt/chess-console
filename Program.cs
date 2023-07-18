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

                        Screen.ShowBoard(game.board);
                        Console.WriteLine();
                        Console.WriteLine("Turn: " + game.turn);
                        Console.Write("Waiting player: ");

                        // Change the text color if current player is the black pieces
                        if (game.currentPlayer == Color.Black)
                        {
                            ConsoleColor aux = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(game.currentPlayer);
                            Console.ForegroundColor = aux;
                        }
                        else
                        {
                            Console.Write(game.currentPlayer);
                        }
                        Console.WriteLine();

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