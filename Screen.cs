﻿using board;

namespace xadrez_console
{
    class Screen
    {
        public static void ShowBoard(Board board) 
        { 
            for (int l = 0; l < board.lines; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.columns; c++) 
                {
                    if (board.Piece(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ShowPiece(board.Piece(l, c));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece.color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
