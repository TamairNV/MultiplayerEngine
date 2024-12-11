
using Raylib_cs;
using System.Numerics;

class Program
{
    static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1600, 800, "Home Made GUI");
        Raylib.SetTargetFPS(60);




        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blue);


            Raylib.EndDrawing();
        }

        // Unload the custom font
        
        Raylib.CloseWindow();
    }










    
    
}





