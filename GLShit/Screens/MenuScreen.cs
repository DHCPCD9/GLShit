using GLShit.Graphics;
using GLShit.Graphics.Shapes;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GLShit.Screens;

public class MenuScreen : Screen
{
   private ExampleBox _box;
    public override void Load(GameBase game)
    {
        base.Load(game);
        
        Add(_box = new ExampleBox());
    }

    public override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        if (e is { Key: Keys.Enter, Alt: true })
        {
            Game.ChangeScreenMode();
        }
        
        if (e is { Key: Keys.Escape })
        {
            Game.Exit();
        }
        
        if (e is { Key: Keys.F12 })
        {
            Game.ScreenShot();
        }

        if (e is { Key: Keys.Right })
        {
            _box.Position.X += 1;
        }
        
        if (e is { Key: Keys.Left })
        {
            _box.Position.X -= 1;
        }
        
        if (e is { Key: Keys.Up })
        {
            _box.Position.Y += 1;
        }
        
        if (e is { Key: Keys.Down })
        {
            _box.Position.Y -= 1;
        }
    }
}