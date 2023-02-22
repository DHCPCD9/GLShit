using OpenTK.Windowing.Common;

namespace GLShit.Screens;

public interface IScreen
{
    public void Load(GameBase game);
    public void Unload();
    
    public void Update();
    
    public void OnKeyDown(KeyboardKeyEventArgs e);
    public void OnKeyUp();
    
    public void OnMouseDown();
    public void OnMouseUp();
    public void OnMouseWheel();
    public void OnMouseMove();
    
    public void Draw();
}