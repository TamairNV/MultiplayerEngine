using System.Numerics;

namespace MultiplayerGame;

public class Camera
{
    public Vector2 Position;
    

    public Camera(int x,int y)
    {
        Position = new Vector2(x, y);
    }
}