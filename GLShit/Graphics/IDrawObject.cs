namespace GLShit.Graphics;

public interface IDrawObject : IDisposable
{
    
    public void Draw();
    public void Update();
    
    public void OnKeyDown();
    public void OnKeyUp();
    
    public void OnMouseDown();
    public void OnMouseUp();
    public void OnMouseWheel();
    
    public void OnMouseMove();


    public void Load(GameBase @base);
    public void Unload();
}