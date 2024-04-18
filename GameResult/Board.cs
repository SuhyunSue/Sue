namespace Games;

public class Board
{
    public Board(string[,] cells)
    {
        Cells = cells;
    }

    public string[,] Cells { get; }

    public int Height => Cells.GetLength(0);
    public int Width => Cells.GetLength(1);

    public void Print()
    {
        Console.WriteLine("________");
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Console.Write("|");
                Console.Write(string.IsNullOrEmpty(Cells[i, j]) ? " " : Cells[i, j]);
            }

            Console.WriteLine("|");
        }

        Console.WriteLine("________");
    }

    public bool IsReachWinning(int winningCondition)
    {
        var count = 0;

        // vertically 
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width - 1; j++)
            {
                if (!string.IsNullOrEmpty(Cells[i, j]) && Cells[i, j] == Cells[i, j + 1])
                {
                    count++;
                }

                if (count >= winningCondition - 1)
                {
                    return true;
                }
            }

            count = 0;
        }

        count = 0;
        // horizontally
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height - 1; j++)
            {
                if (!string.IsNullOrEmpty(Cells[j, i]) && Cells[j, i] == Cells[j + 1, i])
                {
                    count++;
                }

                if (count >= winningCondition - 1)
                {
                    return true;
                }
            }

            count = 0;
        }


        count = 0;

        // diagonally ES  [0,0][1,1][2,2] 

        for (int i = 0; i  < Height - 1 && i < Width - 1; i++)
        {
            if (!string.IsNullOrEmpty(Cells[i , i]) && Cells[i , i] == Cells[i  + 1, i + 1])
            {
                count++;
            }

            if (count >= winningCondition - 1)
            {
                return true;
            }
        }
        


        // diagonally WS  [0,4][1,3][2,2] /
        for (int h = 0, w = Width - 1; w > 1 && h < Height - 1; w--, h++)
        {
            if (!string.IsNullOrEmpty(Cells[h, w]) && Cells[h, w] == Cells[h + 1, w - 1])
            {
                count++;
            }

            if (count >= winningCondition - 1)
            {
                return true;
            }
        }


        return false;
    }

    public void EmptyCell()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Cells[i, j] = "";
            }
        }
    }
}
