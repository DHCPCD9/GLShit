namespace GLShit.Graphics;

public abstract class BaseDrawObject : IDrawObject
{
    
    public GameBase GameBase { get; set; } 
    public void Dispose()
    {
        
    }

    public virtual void Draw()
    {
       
    }

    public virtual void Update()
    {
     
    }

    public virtual void OnKeyDown()
    {
        
    }

    public virtual void OnKeyUp()
    {
       
    }

    public virtual void OnMouseDown()
    {
       
    }

    public virtual void OnMouseUp()
    {
       
    }

    public virtual void OnMouseWheel()
    {
       
    }

    public virtual void OnMouseMove()
    {
       
    }

    public virtual void Load(GameBase @base)
    {
        GameBase = @base;
    }

    public virtual void Unload()
    {
        
    }
}