﻿using board;
using chess;
using System.Text.RegularExpressions;

namespace chess_console
{
    partial class Screen
    {
        public static void ShowGame(ChessGame game)
        {
            ShowBoard(game.Board);
            Console.WriteLine();
            ShowCapturedPieces(game);
            Console.WriteLine();

            if (!game.Finished)
            {
                Console.WriteLine("Turn: " + game.Turn);
                Console.Write("Waiting player: ");

                // Change the text color if current player is the black pieces
                if (game.CurrentPlayer == Color.Black)
                    WriteInBlack(game.CurrentPlayer.ToString());
                else
                    Console.Write(game.CurrentPlayer);

                Console.WriteLine();

                if (game.Check)
                {
                    Console.WriteLine();
                    WriteInRed("CHECK!");
                    Console.WriteLine();
                }
            }
            else
            {
                WriteInRed("CHECKMATE!");
                Console.WriteLine();
                WriteInYellow("Winner: ");

                if (game.CurrentPlayer == Color.Black)
                    WriteInBlack(game.CurrentPlayer.ToString());
                else
                    Console.Write(game.CurrentPlayer);

                Console.WriteLine();
            }
        }

        public static void WriteInBlack(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }

        public static void WriteInRed(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }

        public static void WriteInYellow(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }

        public static void ShowCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("Whites: ");
            ShowSet(game.CapturedPiecesByColor(Color.White));
            WriteInBlack("Blacks: ");
            ShowSet(game.CapturedPiecesByColor(Color.Black));
        }

        public static void ShowSet(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach (Piece piece in pieces)
            {
                if (piece.Color == Color.Black)
                    WriteInBlack(piece.ToString());
                else
                    Console.Write(piece);

                Console.Write(", ");
            }
            Console.WriteLine("]");
        }

        public static void ShowBoard(Board board) 
        { 
            for (int l = 0; l < board.Lines; l++)
            {
                Console.Write(8 - l + " ");

                for (int c = 0; c < board.Columns; c++) 
                    ShowPiece(board.GetPiece(l, c));

                Console.WriteLine();
            }

            WriteInYellow("  a b c d e f g h");
            Console.WriteLine();
        }

        public static void ShowBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor possibleField = ConsoleColor.DarkGray;
            
            for (int l = 0; l < board.Lines; l++)
            {
                Console.Write(8 - l + " ");

                for (int c = 0; c < board.Columns; c++)
                {
                    if (possibleMoves[l,c])
                        Console.BackgroundColor = possibleField;
                    else
                        Console.BackgroundColor = originalBackground;

                    ShowPiece(board.GetPiece(l, c));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }
            WriteInYellow("  a b c d e f g h");
            Console.WriteLine();

            Console.BackgroundColor = originalBackground;
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.Color == Color.White)
                    Console.Write(piece);
                else
                    WriteInBlack(piece.ToString());

                Console.Write(" ");
            }
        }

        [GeneratedRegex("[a-h]{1}[1-8]{1}")]
        private static partial Regex ValidInput();

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine().ToLower();

            // Checking if is a valid input
            if (!(input.Length == 2 && ValidInput().IsMatch(input)))
                throw new InputException($"The input '{input}' is invalid!");

            char cloumn = input[0];
            int line = int.Parse(input[1].ToString());

            return new ChessPosition(cloumn, line);
        }
    }
}
