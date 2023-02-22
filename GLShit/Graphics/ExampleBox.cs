using System.Diagnostics;
using GLShit.Graphics.Shapes;
using OpenTK.Mathematics;

namespace GLShit.Graphics;

public class ExampleBox : Box
{
    public Stopwatch Stopwatch { get; set; } = new();
    public override void Load(GameBase @base)
    {
        Stopwatch.Start();
        base.Load(@base);
    }

    public override void Update()
    {
        var delta = Stopwatch.ElapsedMilliseconds / 1000.0f;
        var colorR = (float) Math.Sin(delta) / 2.0f + 0.5f;
        var colorG = (float) Math.Cos(delta) / 2.0f + 0.5f;
        var colorB = (float) Math.Sin(delta) / 2.0f + 0.5f;
        Color = new Color4(colorR, colorG, colorB, 1.0f);
        base.Update();
    }
}