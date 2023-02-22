

using GLShit;
using OpenTK.Windowing.Desktop;

using (var game = new Game(new GameWindowSettings(),  new NativeWindowSettings
       {
           Title = "GLShit",
           Size = new OpenTK.Mathematics.Vector2i(800, 600),
       }))
{
    game.Run();
}