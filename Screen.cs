using board;
using chess;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace chess_console
{
    class Screen
    {
        public static void ShowGame(ChessGame game)
        {
            ShowBoard(game.board);
            Console.WriteLine();
            ShowCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.turn);
            Console.Write("Waiting player: ");

            // Change the text color if current player is the black pieces
            if (game.currentPlayer == Color.Black)
                WriteInBlack(game.currentPlayer.ToString());
            else
                Console.Write(game.currentPlayer);

            Console.WriteLine();

            if (game.check)
            {
                Console.WriteLine();
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CHECK!");
                Console.ForegroundColor = aux;
            }
        }

        public static void WriteInBlack(string text)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = aux;
        }

        public static void ShowCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("Whites: ");
            ShowSet(game.capturedPiecesByColor(Color.White));
            WriteInBlack("Blacks: ");
            ShowSet(game.capturedPiecesByColor(Color.Black));
        }

        public static void ShowSet(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach (Piece piece in pieces)
            {
                if (piece.color == Color.Black)
                    WriteInBlack(piece.ToString());
                else
                    Console.Write(piece);

                Console.Write(", ");
            }
            Console.WriteLine("]");
        }

        public static void ShowBoard(Board board) 
        { 
            for (int l = 0; l < board.lines; l++)
            {
                Console.Write(8 - l + " ");

                for (int c = 0; c < board.columns; c++) 
                    ShowPiece(board.GetPiece(l, c));

                Console.WriteLine();
            }
            
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;
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
                        Console.BackgroundColor = possibleField;
                    else
                        Console.BackgroundColor = originalBackground;

                    ShowPiece(board.GetPiece(l, c));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;

            Console.BackgroundColor = originalBackground;
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.color == Color.White)
                    Console.Write(piece);
                else
                    WriteInBlack(piece.ToString());

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
