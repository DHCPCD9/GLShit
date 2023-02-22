using System.Diagnostics.CodeAnalysis;
using GLShit.Graphics;
using OpenTK.Windowing.Common;

namespace GLShit.Screens;

public class Screen : IScreen
{
    public GameBase Game { get; set; }
    public List<DrawableObject> DrawObjects { get; set; } = new (); //TODO: Do it in better way

    private GameComponent _gameComponent;
    public virtual void Load(GameBase game)
    {
        Game = game;
        
#if DEBUG
        Console.WriteLine("Loaded screen: " + GetType().Name);
#endif
        //Adding basic game components
        _gameComponent = new GameComponent {
            Position = new Vector(0, 0),
            Size = new Vector(Game.Size.X, Game.Size.Y),
        };
        Add(_gameComponent);
    }

    public virtual void OnResize() {
        _gameComponent.Size = new Vector(Game.Size.X, Game.Size.Y);
    }

    public virtual void Unload()
    {
        DrawObjects.ForEach(x => x.Dispose()); //TODO: Do it in better way too
    }

    public virtual void Update()
    {
        DrawObjects.ForEach(x => x.Update());
    }

    public IDrawObject? GetParentOf(IDrawObject drawableObject)
    {
        //Searching for parent
        foreach (var drawObject in DrawObjects)
        {
            if (drawObject.Children.Any(x => x.Id == drawableObject.Id))
            {
                return drawObject;
            }
        }

        return null;
    }


    public DrawableObject? GetParentOf(DrawableObject drawableObject)
    {
        foreach (var drawObject in DrawObjects)
        {
            if (drawObject.Children.Any(x => x.Id == drawableObject.Id))
            {
                return drawObject;
            }
        }

        return null;
    }

    private DrawableObject? GetHoveredObject()
    {
        var mousePosition = Game.MousePosition;

        var hoveredObject = DrawObjects
            .Where(x => 
                x.Position.X <= mousePosition.X && 
                x.Position.X + x.Size.X >= mousePosition.X &&
                x.Position.Y <= mousePosition.Y && x.Position.Y + x.Size.Y >= mousePosition.Y &&
                x.ZIndex >= 0)
            .OrderByDescending(x => x.ZIndex);

        if (hoveredObject.Any())
        {
            return hoveredObject.First();
        }

        return null;
    }

    public virtual void OnKeyDown(KeyboardKeyEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnKeyDown(e);
        }    
    }

    public virtual void OnKeyUp(KeyboardKeyEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnKeyUp(e);
        }
    }

    public virtual void OnMouseDown(MouseButtonEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnMouseDown(e);
        }
    }

    public virtual void OnMouseUp(MouseButtonEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnMouseUp(e);
        }
    }

    public virtual void OnMouseWheel(MouseWheelEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnMouseWheel(e);
        }
    }

    public virtual void OnMouseMove(MouseMoveEventArgs e)
    {
        if (GetHoveredObject() is { } hoveredObject)
        {
            hoveredObject.OnMouseMove(e);
        }
    }
    
    public void Add(DrawableObject drawObject)
    {
        //Loading the entire object
        drawObject.Load(Game);
        
        //Adding to the main components children
        _gameComponent.Add(drawObject);
    }

    public void Draw()
    {
        DrawObjects.ForEach(x => x.Draw());
    }

}