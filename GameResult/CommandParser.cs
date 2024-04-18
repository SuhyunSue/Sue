

namespace Games;

public static class CommandParser
{
    public static ActionEnum Read(string input)
    {
        if (string.Equals(input, "place",StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Move;
        }
        if (string.Equals(input, "quit", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Quit;
        }
        if (string.Equals(input, "save", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Save;
        }
        if (string.Equals(input, "load", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Load;
        }
        if (string.Equals(input, "help", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Help;
        }
        if (string.Equals(input, "redo", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Redo;
        }
        if (string.Equals(input, "undo", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Undo;
        }
        if (string.Equals(input, "next", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.Next;
        }
        if (string.Equals(input, "new", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.NewGame;
        }
        if (string.Equals(input, "score", StringComparison.InvariantCultureIgnoreCase))
        {
            return ActionEnum.ShowScore;
        }

        return ActionEnum.Unknown;
    }
}
