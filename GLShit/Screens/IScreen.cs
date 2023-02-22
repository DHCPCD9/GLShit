using GLShit.Graphics;
using OpenTK.Windowing.Common;

namespace GLShit.Screens;

public interface IScreen
{
    public void Load(GameBase game);
    public void Unload();
    
    public void Update();
    
    public void OnKeyDown(KeyboardKeyEventArgs e);
    public void OnKeyUp(KeyboardKeyEventArgs e);
    
    public void OnMouseDown(MouseButtonEventArgs e);
    public void OnMouseUp(MouseButtonEventArgs e);
    public void OnMouseWheel(MouseWheelEventArgs e);
    public void OnMouseMove(MouseMoveEventArgs e);
    
    public void Draw();

    public IDrawObject? GetParentOf(IDrawObject drawableObject);

    public DrawableObject? GetParentOf(DrawableObject drawableObject);

}