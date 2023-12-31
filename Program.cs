﻿using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main()
        {

            try
            {
                ChessGame game = new ();

                while (!game.Finished)
                {

                    try
                    {
                        Console.Clear();

                        Screen.ShowGame(game);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = game.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();

                        Screen.ShowBoard(game.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinyPosition(origin, destiny);

                        game.TakeATurn(origin, destiny);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine();
                        Screen.WriteInRed(ex.Message);
                        Console.ReadLine();
                    }
                    catch (InputException ex)
                    {
                        Console.WriteLine();
                        Screen.WriteInRed(ex.Message);
                        Console.ReadLine();
                    }
                }
                
                Console.Clear();
                Screen.ShowGame(game);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Screen.WriteInRed(ex.Message);
                Console.ReadLine();
            }
        }
    }
}