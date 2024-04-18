namespace Games;

public class SosGame : Game
{
    private readonly List<string> _supportSymbol = new() { "S", "O" };

    private readonly List<List<int>> _alreadyGotPoint = new();

    public SosGame(int size)
    {
        Board = new Board(new string[size, size]);
    }

    public SosGame(Game game)
    {
        Board = game.Board;
        CurrentPlayerId = game.CurrentPlayerId;
        NextPlayerId = game.NextPlayerId;
        GameStatus = game.GameStatus;
        Players = game.Players;
    }

    public override int Id => 1;

    public override void CreatePlayers()
    {
        Players = new List<IPlayer>();
        for (var i = 0; i < 2; i++)
        {
            var playerId = i + 1;
            Console.Write("Please chose player{0} type, (1.)human (2.)computer :", playerId);
            var type = Convert.ToInt32(Console.ReadLine());


            Players.Add(type == 1
                ? new HumanPlayer()
                {
                    Id = playerId,
                }
                : new ComputerPlayer()
                {
                    Id = playerId,
                });
        }

        CurrentPlayerId = Players[0].Id;
        NextPlayerId = Players[1].Id;
    }

    protected override string GameInfo =>
        "SOS. Two players take turns to add either an S or an O (no requirement to use the same letter each turn) on a board with at least 3x3 squares in size. If a player makes the sequence SOS vertically, horizontally or diagonally they get a point and also take another turn. Once the grid has been filled up, the winner is the player who made the most SOSs.";

    public override void PlaceMove(string[] input)
    {
        GetCurrentPlayer().ChoseSymbol(_supportSymbol);
        base.PlaceMove(input);
    }


    protected override void CheckWinning()
    {
        //vertically
        for (int j = 0; j < Board.Width; j++)
        {
            for (var i = 0; i < Board.Height - 2; i++)
            {
                if (Board.Cells[i, j] == "S" && Board.Cells[i + 1, j] == "O" && Board.Cells[i + 2, j] == "S")
                {
                    var item = new List<int>() { i, j, i + 1, j, i + 2, j };
                    GotScoreWhenValidated(item);
                }
            }
        }

        //horizontally
        for (int j = 0; j < Board.Height; j++)
        {
            for (var i = 0; i < Board.Width - 2; i++)
            {
                if (Board.Cells[j, i] == "S" && Board.Cells[j, i + 1] == "O" && Board.Cells[j, i + 2] == "S")
                {
                    var item = new List<int>() { j, i, j, i + 1, j, i + 2 };
                    GotScoreWhenValidated(item);
                }
            }
        }

        //diagonally
        for (int i = 0; i < Board.Width - 2; i++)
        {
            for (int j = 0; j < Board.Height - 2 &&  j + 2+i < Board.Width; j++)
            {
                if (Board.Cells[j, j+i] == "S" && Board.Cells[j + 1, j + 1+i] == "O" && Board.Cells[j + 2, j + 2+i] == "S")
                {
                    var item = new List<int>() { j, j + i, j + 1, j + 1 + i, j + 2, j + 2 + i };
                    GotScoreWhenValidated(item);
                }
            }
        }


        foreach (var boardCell in Board.Cells)
        {
            if (string.IsNullOrEmpty(boardCell))
                return;
        }

        GameStatus = GameStatusEnum.Winner;
    }

    private void GotScoreWhenValidated(List<int> item)
    {
        if (!_alreadyGotPoint.Any(x => x.SequenceEqual(item)))
        {
            GetCurrentPlayer().Score++;
            _alreadyGotPoint.Add(item);
        }
    }
}
