// See https://aka.ms/new-console-template for more information


using Games;

Console.WriteLine("Welcome to IFN 563 Games, please enter game code to start game");
Console.Write("(1).SOS (2).Connect Four :");
var game = CreateGame(Convert.ToInt32(Console.ReadLine()));
game.CreatePlayers();

Console.WriteLine("Game Start!");
Console.WriteLine("Player can enter below commend:");
Console.WriteLine("place x y , quit , save , load , help , undo, redo, score. Finish action can enter 'next' to switch player ");
var gameActive = true;
while (gameActive)
{
    if (game.GameStatus != GameStatusEnum.InProgress)
    {
        Console.Write("Game is finished, enter 'new' to start game or enter 'score' to show player score  ");
    }
    
    Console.Write("player{0}: ", game.CurrentPlayerId);

    var input = Console.ReadLine()?.Split(" ");
    if (input != null)
    {
        var action = CommandParser.Read(input[0]);

        if (game.GameStatus == GameStatusEnum.InProgress)
        {
            switch (action)
                    {
                        case ActionEnum.Move:
                            game.PlaceMove(input);
                            break;
                        case ActionEnum.Unknown:
                            Console.WriteLine("no commend can process, please enter again");
                            break;
                        case ActionEnum.Save:
                            FileManager.Save(game);
                            break;
                        case ActionEnum.Load:
                            var load = FileManager.Load();
                            if (load != null)
                            {
                                game = load;
                                game.Board.Print();
                            }
                            else
                            {
                                Console.WriteLine("No game history file can load");
                            }
            
                            break;
                        case ActionEnum.Help:
                            game.ShowHelpInfo();
                            break;
                        case ActionEnum.Undo:
                            game.Undo();
                            game.Board.Print();
                            break;
                        case ActionEnum.Redo:
                            game.Redo();
                            game.Board.Print();
                            break;
                        case ActionEnum.Next:
                            game.SwitchPlayer();
                            break;
                        case ActionEnum.ShowScore:
                            game.ShowScore();
                            break;
                        case ActionEnum.Quit:
                            Console.WriteLine("See you :)");
                            gameActive = false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
        }
        else
        {
            switch (action)
            {
                case ActionEnum.NewGame:
                    game.NewBoard();
                    game.GameStatus = GameStatusEnum.InProgress;
                    break;
                case ActionEnum.ShowScore:
                    game.ShowScore();
                    break;
                case ActionEnum.Quit:
                    Console.WriteLine("See you :)");
                    gameActive = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

Game CreateGame(int gameId)
{
    if (gameId == 1)
    {
        var repeat = true;
        while (repeat)
        {
            Console.Write("Please enter the number for squares size:");
            var size = Convert.ToInt32(Console.ReadLine());
            if (size >= 3)
            {
                repeat = false;
                return new SosGame(size);
            }
            else
                Console.Write("squares size should be more than 3");

        }
       

        
    }

    return new ConnectFour();
}
