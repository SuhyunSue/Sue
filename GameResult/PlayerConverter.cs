using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Games;

public class PlayerConverter:JsonConverter
{
    private readonly Type[] _types;
    public PlayerConverter(params Type[] types)
    {
        _types = types;
    }
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        
        var jsonObject = JObject.Load(reader);
        var player = default(IPlayer);

        var id = jsonObject["Id"].Value<int>();
        var score = jsonObject["Score"].Value<int>();
        var symbol = jsonObject["Symbol"].Value<string>();
        
        switch (jsonObject["Type"].Value<int>())
        {
            case (int)EnumPlayerType.Human:
                player = new HumanPlayer(id,score,symbol);
                break;
            case (int)EnumPlayerType.Computer:
                player = new ComputerPlayer(id,score,symbol);
                break;
        }
        serializer.Populate(jsonObject.CreateReader(), player);
        return player;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IPlayer);
    }
}
