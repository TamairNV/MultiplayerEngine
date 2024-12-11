
using Raylib_cs;
using System.Numerics;
using MultiplayerGame;
using Newtonsoft.Json;


public class Player
{


    public Position Position;
    public WebSocketClient PlayerWebSocket;
    public Player(Position pos)
    {

        PlayerWebSocket = new WebSocketClient(URL:"ws://10.135.217.32:5000/ws/");
        Position = pos;
    }

    public void HandlePlayer()
    {
        Draw();
        moveCamera();
        Vector2 direction = new Vector2(0,0);
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            direction.X -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            direction.X += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            direction.Y -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            direction.Y += 1;
        }

        if (direction.X != 0 || direction.Y != 0)
        {
            Position.WorldPosition += Vector2.Normalize(direction)*5;
        }
        
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)Position.ScreenPosition.X,(int)Position.ScreenPosition.Y,10,Color.Black);
    }
    public static bool isPlayerData(string jsonString)
    {
        try
        {
            JsonConvert.DeserializeObject<PlayerData>(jsonString);
            return true; // Serialization succeeded
        }
        catch (JsonException)
        {
            return false; // Serialization failed
        }
    }

    public async Task GetData()
    {
        PlayerData data = new PlayerData()
        {
            X = Position.WorldPosition.X,
            Y = Position.WorldPosition.Y,
            ID = "0"
        };
        await PlayerWebSocket.sendMessage(JsonConvert.SerializeObject(data));
        
        await PlayerWebSocket.ReceiveDataAsync();
        
        if (PlayerWebSocket.LastMessageReceived != null)
        {
            
            if (isPlayerData(PlayerWebSocket.LastMessageReceived))
            {
            
                PlayerData deserializedData = JsonConvert.DeserializeObject<PlayerData>(PlayerWebSocket.LastMessageReceived);
                foreach (var player in OtherPlayers.Players)
                {
                    if (player.ID == deserializedData.ID)
                    {
                        player.Position.WorldPosition = new Vector2((int)deserializedData.X, (int)deserializedData.Y);
                        return;
                    }
                }
                
                new OtherPlayers(new Position((int)deserializedData.X, (int)deserializedData.Y), deserializedData.ID);
                
            }
        }
    }
    
    


    private void moveCamera()
    {
        Vector2 direction = new Vector2(0,0);
        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            direction.X -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            direction.X += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            direction.Y -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            direction.Y += 1;
        }

        if (direction.X != 0 || direction.Y != 0)
        {
            Position.Camera.Position += Vector2.Normalize(direction)*5;
        }
    }
}

public struct PlayerData
{
    public string ID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

public class OtherPlayers
{
    public static List<OtherPlayers> Players = new List<OtherPlayers>();
    public Position Position;
    public string ID;


    public OtherPlayers(Position position,string ID)
    {
        Position = position;
        this.ID = ID;
        Players.Add(this);
    }

    public void HandlePlayer()
    {
        Draw();
    }

    private void Draw()
    {
        Raylib.DrawCircle((int)Position.ScreenPosition.X,(int)Position.ScreenPosition.Y,10,Color.Black);
    }

    public static void HandlePlayers()
    {
        foreach (var player in Players)
        {
            player.HandlePlayer();
        }
    }
}
