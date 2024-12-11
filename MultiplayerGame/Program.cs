
using Raylib_cs;
using System.Numerics;
using MultiplayerGame;

class Program
{
    static async Task Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1600, 800, "Home Made GUI");
        Raylib.SetTargetFPS(60);
        Camera cam = new Camera(0, 0);
        Position.Camera = cam;
        Position boxPos = new Position(500, 500);

        Player player = new Player(new Position(200,200));

        
        float t = 0;
        while (!Raylib.WindowShouldClose())
        {
            Position.ReDoPositions();
            OtherPlayers.HandlePlayers();
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blue);
            player.HandlePlayer();
            _ = player.GetData();
            
            Raylib.DrawRectangle((int)boxPos.ScreenPosition.X,(int)boxPos.ScreenPosition.Y,100,100,Color.Brown);
            
            
            
            Raylib.EndDrawing();
        }

        // Unload the custom font
        
        Raylib.CloseWindow();
    }










    
    
}





