using OpenTK.Windowing.Common;

namespace GLShit.Graphics;

public interface IDrawObject : IDisposable
{
    public Guid Id { get; set; }
    List<DrawableObject> Children { get; set; }  
    public void Draw();
    public void Update();
    
    public void OnKeyDown(KeyboardKeyEventArgs e);
    public void OnKeyUp(KeyboardKeyEventArgs e);
    
    public void OnMouseDown(MouseButtonEventArgs e);
    public void OnMouseUp(MouseButtonEventArgs e);
    public void OnMouseWheel(MouseWheelEventArgs e);
    public void OnMouseMove(MouseMoveEventArgs e);


    public void Load(GameBase @base);
    public void Unload();
}