using Newtonsoft.Json;

namespace Games;

public class Game
{
    public virtual int Id { get; set; }
    public Board Board { get; set; }
    public int CurrentPlayerId { get; set; }
    public int NextPlayerId { get; set; }

    public List<IPlayer> Players { get; set; }

    protected virtual string GameInfo { get; set; }
    
    protected IPlayer GetCurrentPlayer()
    {
        return Players.First(x => x.Id == CurrentPlayerId);
    }



    public GameStatusEnum GameStatus { get; set; } = GameStatusEnum.InProgress;

    public virtual void PlaceMove(string[] input)
    {
        var success = GetCurrentPlayer().Move(input, Board);
        if (success)
        {
            Board.Print();
            CheckWinning();
        }
    }

    protected virtual void CheckWinning()
    {
    }

    public void SwitchPlayer()
    {
        GetCurrentPlayer().CurrentMove = new int[] { };
        (CurrentPlayerId, NextPlayerId) = (NextPlayerId, CurrentPlayerId);
    }

    public virtual void CreatePlayers()
    {
    }


    public void ShowHelpInfo()
    {
        Console.WriteLine(GameInfo);
    }

    public void Undo()
    {
        var player = GetCurrentPlayer();
        if (player.CurrentMove.Any())
        {
            Board.Cells[player.CurrentMove[0], player.CurrentMove[1]] = "";
        }
        else
        {
            Console.WriteLine("No moving can undo");
        }
    }

    public void Redo()
    {
        var player = GetCurrentPlayer();
        if (player.CurrentMove.Any())
        {
            Board.Cells[player.CurrentMove[0], player.CurrentMove[1]] = player.Symbol;
        }
        else
        {
            Console.WriteLine("No moving can redo");
        }
    }

    public void ShowScore()
    {
        foreach (var player in Players)
        {
            Console.WriteLine($"player{player.Id} score: {player.Score}");
        }
    }

    public void NewBoard()
    {
        Board.EmptyCell();
    }
}
