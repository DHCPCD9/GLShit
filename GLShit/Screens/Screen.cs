using GLShit.Graphics;
using OpenTK.Windowing.Common;

namespace GLShit.Screens;

public class Screen : IScreen
{
    public GameBase Game { get; set; }
    public List<IDrawObject> DrawObjects { get; set; } = new List<IDrawObject>(); //TODO: Do it in better way
    public virtual void Load(GameBase game)
    {
        Game = game;
        
#if DEBUG
        
        Console.WriteLine("Loaded screen: " + GetType().Name);
#endif
    }

    public virtual void Unload()
    {
        DrawObjects.ForEach(x => x.Dispose()); //TODO: Do it in better way too
    }

    public virtual void Update()
    {
        DrawObjects.ForEach(x => x.Update());
    }

    public virtual void OnKeyDown(KeyboardKeyEventArgs e)
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
    
    public void Add(IDrawObject drawObject)
    {
        //Loading the entire object
        drawObject.Load(Game);
        
        DrawObjects.Add(drawObject);
    }

    public void Draw()
    {
        DrawObjects.ForEach(x => x.Draw());
    }
}