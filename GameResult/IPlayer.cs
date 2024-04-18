namespace Games;

public  interface IPlayer
{
    public int Id { get; init; }

    public string Symbol { get; set; }

    public int Score { get; set; }
    
    public  int[] CurrentMove { get; set; }

    public  EnumPlayerType Type { get; }

    public bool Move(string[] input, Board board);
    void ChoseSymbol(List<string> supportSymbol);
}

public enum EnumPlayerType
{
    Human,
    Computer
}

public class HumanPlayer : IPlayer
{

    public int Id { get; init; }
    public string Symbol { get; set; }
    public int Score { get; set; }
    public int[] CurrentMove { get; set; }

    public EnumPlayerType Type => EnumPlayerType.Human;

    public HumanPlayer()
    {
    }

    public HumanPlayer(int id, int score, string symbol)
    {
        Id = id;
        Score = score;
        Symbol = symbol;
    }

    public  bool Move(string[] input, Board board)
    {
        var height = Convert.ToInt32(input[1]);
        var width = Convert.ToInt32(input[2]);
        if (height < board.Height && width < board.Width)
        {
            if (string.IsNullOrEmpty(board.Cells[height, width]))
            {
                board.Cells[height, width] = Symbol;
                CurrentMove = new[] { height, width };
                return true;
            }
            else
            {
                Console.WriteLine("position already have a symbol, please renter again");
                return false;
            }
        }
        else
        {
            Console.WriteLine("position out of board, please renter again");
            return false;
        }
    }

    public void ChoseSymbol(List<string> supportSymbol)
    {
        var validate = false;
        while (!validate)
        {
            Console.Write("Please chose 'S' or 'O' :");
            var symbol = Console.ReadLine() ?? "";
            if (supportSymbol.Contains(symbol))
            {
                Symbol = symbol;
                validate = true;
            }
        }

    }
}

public class ComputerPlayer : IPlayer
{
    public int Id { get; init; }
    public string Symbol { get; set; }
    public int Score { get; set; }
    public int[] CurrentMove { get; set; }
    public EnumPlayerType Type => EnumPlayerType.Computer;


    public ComputerPlayer()
    {
    }

    public ComputerPlayer(int id, int score, string symbol)
    {
        Id = id;
        Score = score;
        Symbol = symbol;
    }
    public  bool Move(string[] input, Board board)
    {
        var position = GeneratePosition(board);
        while (!string.IsNullOrEmpty(board.Cells[position[0], position[1]]))
        {
            position = GeneratePosition(board);
        }

        board.Cells[position[0], position[1]] = Symbol;
        CurrentMove = new[] { position[0], position[1] };

        return true;
    }

    public void ChoseSymbol(List<string> supportSymbol)
    {
        var i = new Random().Next(0, supportSymbol.Count);
        Symbol = supportSymbol[i];
    }

    private static int[] GeneratePosition(Board board)
    {
        var position = new int[2];
        position[0] = new Random().Next(0, board.Height);
        position[1] = new Random().Next(0, board.Width);
        return position;
    }
}
