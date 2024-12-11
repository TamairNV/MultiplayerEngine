using System.Numerics;

namespace MultiplayerGame;
using Raylib_cs;
public class Player
{


    public Position Position;

    public Player(Position pos)
    {
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