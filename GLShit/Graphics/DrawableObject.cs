using OpenTK.Windowing.Common;

namespace GLShit.Graphics;

public abstract class DrawableObject : IDrawObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public GameBase GameBase { get; set; }
    public DrawableObject? Parent { get; set; }
    public List<DrawableObject> Children { get; set; } = new();
    public Vector Position { get; set; } = new();
    public Vector Size { get; set; } = new();
    public Vector Scale { get; set; } = new(1, 1);
    public float Rotation { get; set; } = 0;
    public float Opacity { get; set; } = 1;
    public Axis AutoSizeAxis { get; set; } = Axis.None;
    public int ZIndex { get; set; } = 0;
    public float Width
    {
        get {
            if (AutoSizeAxis == Axis.X || AutoSizeAxis == Axis.Both)
            {
                throw new InvalidTransformationException("Can't get width when AutoSizeAxis is X");
            }

            return Size.X;
        }
        set {
            if (AutoSizeAxis == Axis.X || AutoSizeAxis == Axis.Both)
            {
                throw new InvalidTransformationException("Can't set width when AutoSizeAxis is X");
            }

            Size.X = value;
        }
    }

    public float Height
    {
        get {
            if (AutoSizeAxis == Axis.Y || AutoSizeAxis == Axis.Both)
            {
                throw new InvalidTransformationException("Can't get height when AutoSizeAxis is Y");
            }

            return Size.Y;
        }
        set {
            if (AutoSizeAxis == Axis.Y || AutoSizeAxis == Axis.Both)
            {
                throw new InvalidTransformationException("Can't set height when AutoSizeAxis is Y");
            }

            Size.Y = value;
        }
    }

    
    public virtual void Draw()
    {

    }

    public virtual void Update()
    {
        //Updating size
        if (AutoSizeAxis != Axis.None)
        {
            var size = GetAutoSize();
            Size = size;
        }

        //Updating children
        foreach (var child in Children)
        {
            child.Update();
        }
    }

    private Vector GetAutoSize()
    {
        if (AutoSizeAxis == Axis.None)
        {
            throw new InvalidTransformationException("Can't get auto size axis when AutoSizeAxis is None");
        }

        var parent = GameBase.ScreenStack.CurrentScreen.GetParentOf(this);
        if (parent == null)
        {
            throw new InvalidTransformationException("Can't get auto size axis when parent is null");
        }

        var size = new Vector();
        if (AutoSizeAxis == Axis.X)
        {
            size.X = parent.Size.X;
            size.Y = Size.Y;
        }
        if (AutoSizeAxis == Axis.Y)
        {
            size.X = Size.X;
            size.Y = parent.Size.Y;
        }

        if (AutoSizeAxis == Axis.Both)
        {
            size.X = parent.Size.X;
            size.Y = parent.Size.Y;
        }

        return size;
    }

    public virtual void OnKeyDown(KeyboardKeyEventArgs e)
    {

    }

    public virtual void OnKeyUp(KeyboardKeyEventArgs e)
    {

    }

    public virtual void OnMouseDown(MouseButtonEventArgs e)
    {

    }

    public virtual void OnMouseUp(MouseButtonEventArgs e)
    {

    }

    public virtual void OnMouseWheel(MouseWheelEventArgs e)
    {

    }

    public virtual void OnMouseMove(MouseMoveEventArgs e)
    {

    }

    public virtual void Load(GameBase @base)
    {
        GameBase = @base;

        //Setting parent
        Parent = @base.ScreenStack.CurrentScreen.GetParentOf(this);

    }

    public virtual void Unload()
    {

    }

    public virtual void Dispose()
    {

    }
}