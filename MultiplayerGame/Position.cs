using System.Numerics;

namespace MultiplayerGame;

public class Position
{

    private static List<Position> Positions = new List<Position>();
    public static Camera Camera;
    public Vector2 WorldPosition;
    public Vector2 ScreenPosition;
    
    
    public  Position(int X, int Y)
    {
        WorldPosition = new Vector2(X, Y);
        Positions.Add(this);
    }

    private void ReDoPosition()
    {
        ScreenPosition = WorldPosition - Camera.Position;
    }

    public static void ReDoPositions()
    {
        foreach (var position in Positions)
        {
            position.ReDoPosition();
            
        }   
    }
    
}