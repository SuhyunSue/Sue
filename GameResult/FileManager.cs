using System.Text;
using Newtonsoft.Json;

namespace Games;

public static class FileManager
{
    private static readonly string Path = AppDomain.CurrentDomain.BaseDirectory + @"GameHistory.txt";

    public static void Save(Game game)
    {
        var fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
        var streamWriter = new StreamWriter(fileStream);
        streamWriter.BaseStream.Seek(0, SeekOrigin.End);
        var serializeObject = JsonConvert.SerializeObject(game);
        streamWriter.Write(serializeObject);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static Game? Load()
    {
        var fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
        var text = streamReader.ReadToEnd();
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        var game = JsonConvert.DeserializeObject<Game>(text, new PlayerConverter());
        if (game != null)
        {
            return game.Id == 1 ? new SosGame(game) : new ConnectFour(game);
        }

        return null;
    }
}
