using board;
using chess;

namespace chess_console
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
                    ShowPiece(board.GetPiece(l, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ShowBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor possibleField = ConsoleColor.DarkGray;
            
            for (int l = 0; l < board.lines; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < board.columns; c++)
                {
                    if (possibleMoves[l,c])
                    {
                        Console.BackgroundColor = possibleField;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }

                    ShowPiece(board.GetPiece(l, c));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

            Console.BackgroundColor = originalBackground;
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine().ToLower();
            char cloumn = input[0];
            int line = int.Parse(input[1].ToString());

            return new ChessPosition(cloumn, line);
        }
    }
}
