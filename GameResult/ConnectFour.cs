namespace Games;

public class ConnectFour : Game
{
    public ConnectFour()
    {
        Board = new Board(new string[7, 6]);
    }

    public ConnectFour(Game game)
    {
        Board = game.Board;
        Players = game.Players;
        CurrentPlayerId = game.CurrentPlayerId;
        NextPlayerId = game.NextPlayerId;
        GameStatus = game.GameStatus;
        Players = game.Players;
    }

    public override int Id => 2;
    private int WinningCondition => 4;

    protected override string GameInfo =>
        "Connect Four aka Four in a Row. Two players take turns dropping pieces on a 7x6 board. The player forms an unbroken chain of four pieces horizontally, vertically, or diagonally, wins the game.";

    public override void CreatePlayers()
    {
        Players = new List<IPlayer>();
        for (var i = 0; i < 2; i++)
        {
            var playerId = i + 1;
            Console.Write("Please chose player{0} type, (1.)human (2.)computer :", playerId);
            var type = Convert.ToInt32(Console.ReadLine());

            Console.Write("Please chose symbol for player {0} :", playerId);
            var symbol = Console.ReadLine() ?? (playerId == 1 ? "X" : "O");


            Players.Add(type == 1
                ? new HumanPlayer()
                {
                    Id = playerId,
                    Symbol = symbol
                }
                : new ComputerPlayer()
                {
                    Id = playerId,
                    Symbol = symbol
                });
        }

        CurrentPlayerId = Players[0].Id;
        NextPlayerId = Players[1].Id;
    }

    protected override void CheckWinning()
    {
        var isWin = Board.IsReachWinning(WinningCondition) ||
                    Board.IsReachWinning(WinningCondition);
        if (isWin)
        {
            Console.WriteLine("player{0} is win!!", CurrentPlayerId);
            GameStatus = GameStatusEnum.Winner;
        }
        else
        {
            foreach (var boardCell in Board.Cells)
            {
                if (string.IsNullOrEmpty(boardCell))
                    return;
            }

            Console.WriteLine("Draw!");
            GameStatus = GameStatusEnum.Draw;
        }
    }
}
