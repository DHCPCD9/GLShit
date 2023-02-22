
using GLShit;
using GLShit.Graphics;
using GLShit.Graphics.Transformations;
using OpenTK.Mathematics;

public class Transformation {
    public TransformationType Type { get; set; }
    
    public float Duration { get; set; }
    
    public float Elapsed { get; set; }
    public GameBase Game { get; set; }
    public float Start { get; set; }
    public float End { get; set; }
    public Color4 ColorToFade { get; set; }

    public Transformation(GameBase @base)
    {
        Game = @base;
    }
    
    public static float Tween(float start, float end, float elapsed, float duration) {
        return start + (end - start) * elapsed / duration;
    }
    
    public static Color4 TweenColor(Color4 start, Color4 end, float elapsed, float duration) {
        var rStart = start.R;
        var rEnd = end.R;
        var gStart = start.G;
        var gEnd = end.G;
        var bStart = start.B;
        var bEnd = end.B;
        
        var r = rStart + (rEnd - rStart) * elapsed / duration;
        var g = gStart + (gEnd - gStart) * elapsed / duration;
        var b = bStart + (bEnd - bStart) * elapsed / duration;
        
        return new Color4(r, g, b, 1.0f);
    }
    
    public virtual void Update(DrawableObject drawable) {
        Elapsed += Game.DeltaTime;
        var current = Tween(Start, End, Elapsed, Duration);
        
        
        switch (Type) {
            case TransformationType.Fade:
                drawable.Opacity = current;
                break;
            case TransformationType.Scale:
                drawable.Scale = new Vector(current, current);
                break;
            case TransformationType.Rotate:
                drawable.Opacity = current;
                break;
            case TransformationType.Color:
                var currentColor = TweenColor(drawable.Color, ColorToFade, Elapsed, Duration);
                drawable.Color = currentColor;
                break;
            case TransformationType.Move:
                drawable.Position = new Vector(current, current);
                break;
        }
        
        if (Elapsed >= Duration) {
            drawable.Transformations.Remove(this);
        }
    }


}