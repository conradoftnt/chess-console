using board;

namespace xadrez_console
{
    class Screen
    {
        public static void ShowBoard(Board brd) 
        { 
            for (int l = 0; l < brd.lines; l++)
            {
                for(int c = 0; c < brd.cloumns; c++) 
                {
                    if (brd.Piece(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(brd.Piece(l, c) + " ");
                    }
                }
                Console.WriteLine();
            }    
        }
    }
}
